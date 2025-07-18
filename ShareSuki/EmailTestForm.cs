using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareSuki
{
    public partial class EmailTestForm : Form
    {
        public EmailTestForm()
        {
            InitializeComponent();
        }

        private void EmailTestForm_Load(object sender, EventArgs e)
        {
            // 設定ファイルから初期値を読み込み
            txtSmtpServer.Text = EmailConfig.SmtpServer;
            txtSmtpPort.Text = EmailConfig.SmtpPort.ToString();
            txtFromEmail.Text = EmailConfig.FromEmail;
            txtUsername.Text = EmailConfig.Username;
            txtPassword.Text = EmailConfig.Password;
        }

        private async void btnSendEmail_Click(object sender, EventArgs e)
        {
            // 入力値の検証
            if (string.IsNullOrWhiteSpace(txtToEmail.Text))
            {
                MessageBox.Show("送信先メールアドレスを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("件名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSmtpServer.Text) || 
                string.IsNullOrWhiteSpace(txtFromEmail.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("SMTP設定をすべて入力してください。", "設定エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSmtpPort.Text, out int port))
            {
                MessageBox.Show("ポート番号は数値で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSmtpPort.Focus();
                return;
            }

            // 送信実行
            btnSendEmail.Enabled = false;
            lblStatus.Text = "メール送信中...";
            lblStatus.ForeColor = System.Drawing.Color.Blue;

            try
            {
                // EmailServiceインスタンス作成
                var emailService = new EmailService(
                    txtSmtpServer.Text,
                    port,
                    txtFromEmail.Text,
                    "ShareSuki",
                    txtUsername.Text,
                    txtPassword.Text
                );

                // メール送信
                bool success = await emailService.SendEmailAsync(
                    txtToEmail.Text,
                    txtToName.Text,
                    txtSubject.Text,
                    txtBody.Text
                );

                if (success)
                {
                    lblStatus.Text = "メールの送信が完了しました。";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    MessageBox.Show("メールを正常に送信しました。", "送信完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "メールの送信に失敗しました。";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    MessageBox.Show("メールの送信に失敗しました。SMTP設定を確認してください。", "送信エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"エラー: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show($"メール送信中にエラーが発生しました。\\n\\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSendEmail.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
