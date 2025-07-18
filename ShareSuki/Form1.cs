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
            InitializeComboBoxes();
            InitializeDatabase();
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
                "数学", "英語", "国語", "理科", "社会", 
                "プログラミング", "デザイン", "音楽", "美術", "体育",
                "料理", "手芸", "写真", "動画編集", "イラスト",
                "楽器演奏", "ダンス", "書道", "語学", "簿記"
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
                // マッチング結果表示フォームを作成して表示
                Form matchForm = new Form();
                matchForm.Text = "マッチング結果";
                matchForm.Size = new Size(500, 400);
                matchForm.StartPosition = FormStartPosition.CenterParent;

                TextBox txtMatch = new TextBox();
                txtMatch.Multiline = true;
                txtMatch.ScrollBars = ScrollBars.Vertical;
                txtMatch.ReadOnly = true;
                txtMatch.Dock = DockStyle.Fill;
                txtMatch.Text = matchInfo;
                txtMatch.Font = new Font("MS Gothic", 9);

                matchForm.Controls.Add(txtMatch);
                matchForm.ShowDialog(this);
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

        // メールテストボタンのイベントハンドラー
        private void btnEmailTest_Click(object sender, EventArgs e)
        {
            EmailTestForm emailTestForm = new EmailTestForm();
            emailTestForm.ShowDialog(this);
        }
    }
}
