using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareSuki
{
    public partial class Form1 : Form
    {
        private SkillDataService _skillDataService;

        public Form1()
        {
            InitializeComponent();
            _skillDataService = new SkillDataService();
            this.Size = new Size(550, 600); // ← 必要に応じてサイズを調整
            InitializeComboBoxes();
            InitializeDatabase();
            
            // フォーム終了時のイベントハンドラを設定
            this.FormClosed += Form1_FormClosed;
        }

        private void InitializeDatabase()
        {
            try
            {
                DbHelper.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("データベースの初期化中にエラーが発生しました: " + ex.Message, 
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComboBoxes()
        {
            // クラス名の選択肢
            cmbClassName.Items.AddRange(new string[] { 
                "NF1", "SF1", "SF2", 
                "TF1", "JF1", "NS1", 
                "SS1", "SS2", "NT1","NV1" 
            });

            // 性別の選択肢
            cmbGender.Items.AddRange(new string[] { "男性", "女性", "その他" });

            // スキルの選択肢
            string[] skills = {
                "HTML", "CSS", "JavaScript", "TypeScript",
                "PHP", "Ruby", "Python", "Java", "Node.js", "Go", "Perl",
                "Swift", "Kotlin", "Objective-C", "Dart",
                "C", "C++", "C#", "Lua", "GDScript", "Rust",
                "R", "Julia", "MATLAB", "Scala",
                "Shell Script", "PowerShell", "VBA", "SQL",
                "Visual Basic .NET", "Delphi", "Object Pascal",
                "Solidity", "Vyper", "Google Apps Script", "Scratch", "Blockly"
            };
            cmbRequestedSkill.Items.AddRange(skills);
            cmbOfferedSkill.Items.AddRange(skills);

            // 待機期間の選択肢
            cmbWaitingPeriod.Items.AddRange(new string[] { 
                "1週間以内", "2週間以内", "1ヶ月以内", "3ヶ月以内", "期間指定なし" 
            });
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 入力値のバリデーション
            if (!ValidateInputs())
            {
                return;
            }

            SkillData newSkillData = new SkillData
            {
                学籍番号 = int.Parse(txtStudentId.Text),
                クラス名 = cmbClassName.SelectedItem?.ToString(),
                出席番号 = string.IsNullOrEmpty(txtAttendanceNo.Text) ? (int?)null : int.Parse(txtAttendanceNo.Text),
                性別 = cmbGender.SelectedItem?.ToString(),
                氏名 = txtName.Text,
                メールアドレス = txtEmail.Text,
                求スキル = cmbRequestedSkill.SelectedItem?.ToString(),
                求スキル備考 = txtRequestedSkillNote.Text,
                譲スキル = cmbOfferedSkill.SelectedItem?.ToString(),
                譲スキル備考 = txtOfferedSkillNote.Text,
                待機期間 = cmbWaitingPeriod.SelectedItem?.ToString()
            };

            try
            {
                int newId = _skillDataService.AddSkillData(newSkillData);
                newSkillData.ID = newId;
                
                MessageBox.Show("スキル情報が正常に登録されました。自動マッチングを開始します。", 
                    "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 自動マッチング処理の呼び出し
                PerformAutoMatching(newSkillData);

                // フォームのリセット
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("スキル情報の登録中にエラーが発生しました: " + ex.Message, 
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // 学籍番号の6桁制限
            if (string.IsNullOrWhiteSpace(txtStudentId.Text) || txtStudentId.Text.Length != 6 || !int.TryParse(txtStudentId.Text, out _))
            {
                MessageBox.Show("学籍番号は6桁の数字で入力してください。", 
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentId.Focus();
                return false;
            }

            // 氏名の必須チェック
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("氏名を入力してください。", 
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // 出席番号が入力されている場合の数値チェック
            if (!string.IsNullOrEmpty(txtAttendanceNo.Text) && !int.TryParse(txtAttendanceNo.Text, out _))
            {
                MessageBox.Show("出席番号は数字で入力してください。", 
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAttendanceNo.Focus();
                return false;
            }

            return true;
        }

        private void PerformAutoMatching(SkillData registeredSkillData)
        {
            List<SkillData> matches = new List<SkillData>();
            List<SkillData> reverseMatches = new List<SkillData>();

            // 求スキルに対するマッチング
            if (!string.IsNullOrEmpty(registeredSkillData.求スキル))
            {
                matches = _skillDataService.FindMatchingSkills(registeredSkillData.求スキル, registeredSkillData.ID);
            }

            // 譲スキルに対する逆マッチング
            if (!string.IsNullOrEmpty(registeredSkillData.譲スキル))
            {
                reverseMatches = _skillDataService.FindReverseMatchingSkills(registeredSkillData.譲スキル, registeredSkillData.ID);
            }

            string matchInfo = "";

            if (matches.Any())
            {
                matchInfo += "【あなたの求スキルにマッチする人が見つかりました】\n";
                matchInfo += $"求スキル「{registeredSkillData.求スキル}」を教えてくれる人:\n\n";
                foreach (var match in matches)
                {
                    matchInfo += $"・氏名: {match.氏名}\n";
                    matchInfo += $"  クラス: {match.クラス名}\n";
                    matchInfo += $"  譲スキル: {match.譲スキル}\n";
                    if (!string.IsNullOrEmpty(match.譲スキル備考))
                        matchInfo += $"  備考: {match.譲スキル備考}\n";
                    matchInfo += "\n";
                }
            }

            if (reverseMatches.Any())
            {
                if (!string.IsNullOrEmpty(matchInfo))
                    matchInfo += "\n" + new string('-', 50) + "\n\n";

                matchInfo += "【あなたの譲スキルを求めている人が見つかりました】\n";
                matchInfo += $"譲スキル「{registeredSkillData.譲スキル}」を求めている人:\n\n";
                foreach (var match in reverseMatches)
                {
                    matchInfo += $"・氏名: {match.氏名}\n";
                    matchInfo += $"  クラス: {match.クラス名}\n";
                    matchInfo += $"  求スキル: {match.求スキル}\n";
                    if (!string.IsNullOrEmpty(match.求スキル備考))
                        matchInfo += $"  備考: {match.求スキル備考}\n";
                    matchInfo += "\n";
                }
            }

            if (!string.IsNullOrEmpty(matchInfo))
            {
                // スタイルの良いマッチング結果表示フォームを作成
                ShowStylishMatchingResults(matches, reverseMatches, registeredSkillData);
            }
            else
            {
                MessageBox.Show("現在、マッチするユーザーは見つかりませんでした。\n新しい登録があるとマッチングされる可能性があります。", 
                    "マッチング結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearForm()
        {
            txtStudentId.Clear();
            cmbClassName.SelectedIndex = -1;
            txtAttendanceNo.Clear();
            cmbGender.SelectedIndex = -1;
            txtName.Clear();
            txtEmail.Clear();
            cmbRequestedSkill.SelectedIndex = -1;
            txtRequestedSkillNote.Clear();
            cmbOfferedSkill.SelectedIndex = -1;
            txtOfferedSkillNote.Clear();
            cmbWaitingPeriod.SelectedIndex = -1;
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            // 登録データ一覧表示フォームを開く
            DataViewForm dataViewForm = new DataViewForm();
            dataViewForm.ShowDialog(this);
        }

        // 学籍番号入力欄で数字のみを許可
        private void txtStudentId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // 出席番号入力欄で数字のみを許可
        private void txtAttendanceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// スタイリッシュなマッチング結果表示フォームを作成・表示します
        /// </summary>
        private void ShowStylishMatchingResults(List<SkillData> matches, List<SkillData> reverseMatches, SkillData registeredSkillData)
        {
            // 画面サイズを取得して適切なフォームサイズを決定
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            int maxFormHeight = workingArea.Height - 120; // タスクバーやタイトルバーの余白
            int maxFormWidth = Math.Min(850, workingArea.Width - 100);
            
            // マッチ数に応じてパネルの高さを正確に計算
            int totalMatches = matches.Count + reverseMatches.Count;
            int headerHeight = 70; // タイトル + サブタイトル
            int buttonHeight = 20;  // 閉じるボタンエリア
            int sectionHeaderHeight = 40; // セクションヘッダー1つあたり
            int panelHeight = 75;   // 各マッチパネルの高さ（さらにコンパクト化）
            int panelGap = 8;       // パネル間の間隔
            int separatorHeight = matches.Any() && reverseMatches.Any() ? 20 : 0;
            int contentPadding = 30; // コンテンツエリアの上下パディング
            
            // セクション数の計算
            int sectionCount = (matches.Any() ? 1 : 0) + (reverseMatches.Any() ? 1 : 0);
            
            int requiredContentHeight = headerHeight + 
                                      (sectionCount * sectionHeaderHeight) + 
                                      (totalMatches * (panelHeight + panelGap)) + 
                                      separatorHeight + 
                                      buttonHeight + 
                                      contentPadding;
            
            int formHeight = Math.Min(maxFormHeight, Math.Max(400, requiredContentHeight + 50));
            
            Form matchForm = new Form();
            matchForm.Text = "✨ スキルマッチング結果";
            matchForm.Size = new Size(maxFormWidth, formHeight+40);
            matchForm.StartPosition = FormStartPosition.CenterParent;
            matchForm.BackColor = Color.FromArgb(245, 248, 250);
            matchForm.Font = new Font("Yu Gothic UI", 9F);
            matchForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            matchForm.MaximizeBox = false;
            matchForm.MinimizeBox = false;

            // メインパネル
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(15);
            mainPanel.BackColor = Color.FromArgb(245, 248, 250);

            // タイトルラベル
            Label titleLabel = new Label();
            titleLabel.Text = "🎯 スキルマッチングが見つかりました！";
            titleLabel.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(46, 125, 50);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(5, 5);

            // サブタイトル
            Label subTitleLabel = new Label();
            subTitleLabel.Text = $"見つかったマッチ: {totalMatches}件";
            subTitleLabel.Font = new Font("Yu Gothic UI", 9F);
            subTitleLabel.ForeColor = Color.FromArgb(84, 110, 122);
            subTitleLabel.AutoSize = true;
            subTitleLabel.Location = new Point(5, 30);

            // コンテンツエリア（スクロール無し、コンパクト表示）
            Panel contentArea = new Panel();
            contentArea.Location = new Point(5, 55);
            contentArea.Size = new Size(maxFormWidth - 40, formHeight - 120);
            contentArea.BackColor = Color.White;
            contentArea.BorderStyle = BorderStyle.FixedSingle;
            contentArea.Padding = new Padding(10);
            contentArea.AutoScroll = false; // スクロール無効化

            int yPosition = 10;
            int contentWidth = contentArea.Width - 30;

            // 求スキルマッチ
            if (matches.Any())
            {
                Label sectionLabel1 = new Label();
                sectionLabel1.Text = "📚 あなたの求スキルを教えてくれる人";
                sectionLabel1.Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold);
                sectionLabel1.ForeColor = Color.FromArgb(25, 118, 210);
                sectionLabel1.AutoSize = true;
                sectionLabel1.Location = new Point(8, yPosition);
                contentArea.Controls.Add(sectionLabel1);
                yPosition += 25;

                Label skillLabel1 = new Label();
                skillLabel1.Text = $"求めているスキル: 「{registeredSkillData.求スキル}」";
                skillLabel1.Font = new Font("Yu Gothic UI", 8F);
                skillLabel1.ForeColor = Color.FromArgb(84, 110, 122);
                skillLabel1.AutoSize = true;
                skillLabel1.Location = new Point(20, yPosition);
                contentArea.Controls.Add(skillLabel1);
                yPosition += 25;

                foreach (var match in matches)
                {
                    Panel matchPanel = CreateMatchPanel(match, true, contentWidth);
                    matchPanel.Location = new Point(8, yPosition);
                    contentArea.Controls.Add(matchPanel);
                    yPosition += panelHeight + panelGap;
                }

                if (reverseMatches.Any())
                    yPosition += 10;
            }

            // 譲スキルマッチ
            if (reverseMatches.Any())
            {
                if (matches.Any())
                {
                    // セパレーター
                    Panel separator = new Panel();
                    separator.Location = new Point(8, yPosition);
                    separator.Size = new Size(contentWidth - 16, 1);
                    separator.BackColor = Color.FromArgb(224, 224, 224);
                    contentArea.Controls.Add(separator);
                    yPosition += 15;
                }

                Label sectionLabel2 = new Label();
                sectionLabel2.Text = "🎓 あなたの譲スキルを求めている人";
                sectionLabel2.Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold);
                sectionLabel2.ForeColor = Color.FromArgb(156, 39, 176);
                sectionLabel2.AutoSize = true;
                sectionLabel2.Location = new Point(8, yPosition);
                contentArea.Controls.Add(sectionLabel2);
                yPosition += 25;

                Label skillLabel2 = new Label();
                skillLabel2.Text = $"提供できるスキル: 「{registeredSkillData.譲スキル}」";
                skillLabel2.Font = new Font("Yu Gothic UI", 8F);
                skillLabel2.ForeColor = Color.FromArgb(84, 110, 122);
                skillLabel2.AutoSize = true;
                skillLabel2.Location = new Point(20, yPosition);
                contentArea.Controls.Add(skillLabel2);
                yPosition += 25;

                foreach (var match in reverseMatches)
                {
                    Panel matchPanel = CreateMatchPanel(match, false, contentWidth);
                    matchPanel.Location = new Point(8, yPosition);
                    contentArea.Controls.Add(matchPanel);
                    yPosition += panelHeight + panelGap;
                }
            }

            // 閉じるボタン
            Button closeButton = new Button();
            closeButton.Text = "閉じる";
            closeButton.Size = new Size(80, 30);
            closeButton.Location = new Point(maxFormWidth - 110, formHeight - 50);
            closeButton.BackColor = Color.FromArgb(66, 165, 245);
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            closeButton.Cursor = Cursors.Hand;
            closeButton.Click += (s, e) => matchForm.Close();

            // コントロールをフォームに追加
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(subTitleLabel);
            mainPanel.Controls.Add(contentArea);
            mainPanel.Controls.Add(closeButton);
            matchForm.Controls.Add(mainPanel);

            matchForm.ShowDialog(this);
        }

        /// <summary>
        /// 個別のマッチ結果パネルを作成します
        /// </summary>
        private Panel CreateMatchPanel(SkillData match, bool isRequestMatch, int panelWidth = 600)
        {
            // パネル幅を適切に設定（右側の余白を回避）
            int adjustedWidth = Math.Max(550, panelWidth - 20);
            
            Panel panel = new Panel();
            panel.Size = new Size(adjustedWidth, 75); // 高さをコンパクト化
            panel.BackColor = Color.FromArgb(250, 250, 250);
            panel.BorderStyle = BorderStyle.FixedSingle;

            // 左側のカラーバー（確実に見えるよう配置）
            Panel colorBar = new Panel();
            colorBar.Size = new Size(3, 75);
            colorBar.Location = new Point(5, 0); // 左マージンを設ける
            colorBar.BackColor = isRequestMatch ? Color.FromArgb(25, 118, 210) : Color.FromArgb(156, 39, 176);
            panel.Controls.Add(colorBar);

            // 名前ラベル
            Label nameLabel = new Label();
            nameLabel.Text = $"👤 {match.氏名}";
            nameLabel.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            nameLabel.ForeColor = Color.FromArgb(33, 33, 33);
            nameLabel.Location = new Point(18, 8); // カラーバーより右に配置
            nameLabel.AutoSize = true;
            panel.Controls.Add(nameLabel);

            // クラス・出席番号
            Label classLabel = new Label();
            string classInfo = $"🏫 {match.クラス名}";
            if (match.出席番号.HasValue)
                classInfo += $" ({match.出席番号}番)";
            classLabel.Text = classInfo;
            classLabel.Font = new Font("Yu Gothic UI", 8F);
            classLabel.ForeColor = Color.FromArgb(84, 110, 122);
            classLabel.Location = new Point(18, 26);
            classLabel.AutoSize = true;
            panel.Controls.Add(classLabel);

            // スキル情報
            Label skillLabel = new Label();
            if (isRequestMatch)
            {
                skillLabel.Text = $"💡 提供スキル: {match.譲スキル}";
            }
            else
            {
                skillLabel.Text = $"🎯 求めるスキル: {match.求スキル}";
            }
            skillLabel.Font = new Font("Yu Gothic UI", 8F, FontStyle.Bold);
            skillLabel.ForeColor = isRequestMatch ? Color.FromArgb(25, 118, 210) : Color.FromArgb(156, 39, 176);
            skillLabel.Location = new Point(18, 42);
            skillLabel.Size = new Size(adjustedWidth - 200, 15); // 幅を制限
            panel.Controls.Add(skillLabel);

            // 備考（短縮表示）
            string note = isRequestMatch ? match.譲スキル備考 : match.求スキル備考;
            if (!string.IsNullOrEmpty(note))
            {
                Label noteLabel = new Label();
                string displayNote = note.Length > 25 ? note.Substring(0, 25) + "..." : note;
                noteLabel.Text = $"📝 {displayNote}";
                noteLabel.Font = new Font("Yu Gothic UI", 7F);
                noteLabel.ForeColor = Color.FromArgb(117, 117, 117);
                noteLabel.Location = new Point(18, 57);
                noteLabel.Size = new Size(200, 12);
                panel.Controls.Add(noteLabel);
            }

            // 右側エリア - 待機期間とメール
            int rightX = adjustedWidth - 180;
            
            // 待機期間
            if (!string.IsNullOrEmpty(match.待機期間))
            {
                Label waitLabel = new Label();
                waitLabel.Text = $"⏰ {match.待機期間}";
                waitLabel.Font = new Font("Yu Gothic UI", 7F);
                waitLabel.ForeColor = Color.FromArgb(117, 117, 117);
                waitLabel.Location = new Point(rightX, 26);
                waitLabel.AutoSize = true;
                panel.Controls.Add(waitLabel);
            }

            // メールアドレス（あれば）
            if (!string.IsNullOrEmpty(match.メールアドレス))
            {
                Label emailLabel = new Label();
                string displayEmail = match.メールアドレス.Length > 20 ? match.メールアドレス.Substring(0, 20) + "..." : match.メールアドレス;
                emailLabel.Text = $"📧 {displayEmail}";
                emailLabel.Font = new Font("Yu Gothic UI", 7F);
                emailLabel.ForeColor = Color.FromArgb(25, 118, 210);
                emailLabel.Location = new Point(rightX, 42);
                emailLabel.Size = new Size(170, 12);
                emailLabel.Cursor = Cursors.Hand;
                panel.Controls.Add(emailLabel);
            }

            return panel;
        }
        
        /// <summary>
        /// フォーム終了時のクリーンアップ処理
        /// </summary>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // データベースのクリーンアップ処理を実行
                DbHelper.CleanupDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"フォーム終了時のクリーンアップエラー: {ex.Message}");
            }
        }
    }
}
