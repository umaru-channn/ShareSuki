using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ShareSuki
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string smtpServer, int smtpPort, string fromEmail, string fromName, string username, string password)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _fromEmail = fromEmail;
            _fromName = fromName;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// テキストメールを送信します
        /// </summary>
        /// <param name="toEmail">送信先メールアドレス</param>
        /// <param name="toName">送信先名前</param>
        /// <param name="subject">件名</param>
        /// <param name="body">本文</param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string body)
        {
            try
            {
                // MimeMessageを作成し、宛先やタイトルなどを設定する
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_fromName, _fromEmail));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;

                // 本文を作成
                var textPart = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = body
                };
                message.Body = textPart;

                // SMTPクライアントを使用してメールを送信
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_username, _password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                // エラーログを出力（実際の運用では適切なログ出力を行う）
                Console.WriteLine($"メール送信エラー: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// HTMLメールを送信します
        /// </summary>
        /// <param name="toEmail">送信先メールアドレス</param>
        /// <param name="toName">送信先名前</param>
        /// <param name="subject">件名</param>
        /// <param name="htmlBody">HTML本文</param>
        /// <returns></returns>
        public async Task<bool> SendHtmlEmailAsync(string toEmail, string toName, string subject, string htmlBody)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_fromName, _fromEmail));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;

                // HTML本文を作成
                var htmlPart = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlBody
                };
                message.Body = htmlPart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_username, _password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTMLメール送信エラー: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// スキルマッチング通知メールを送信します
        /// </summary>
        /// <param name="toSkillData">送信先のスキルデータ</param>
        /// <param name="matchingSkillData">マッチしたスキルデータ</param>
        /// <returns></returns>
        public async Task<bool> SendSkillMatchNotificationAsync(SkillData toSkillData, SkillData matchingSkillData)
        {
            if (string.IsNullOrEmpty(toSkillData.メールアドレス))
            {
                return false;
            }

            string subject = "【ShareSuki】スキルマッチングのお知らせ";
            string body = $@"
{toSkillData.氏名} 様

いつもShareSukiをご利用いただき、ありがとうございます。

あなたが求めているスキル「{toSkillData.求スキル}」に関して、
マッチする可能性のある方が見つかりました。

■マッチした方の情報：
お名前: {matchingSkillData.氏名}
クラス: {matchingSkillData.クラス名}
提供可能スキル: {matchingSkillData.譲スキル}
備考: {matchingSkillData.譲スキル備考}
待機期間: {matchingSkillData.待機期間}

詳細については、直接お相手の方にご連絡ください。

---
ShareSuki運営チーム
";

            return await SendEmailAsync(toSkillData.メールアドレス, toSkillData.氏名, subject, body);
        }
    }
}
