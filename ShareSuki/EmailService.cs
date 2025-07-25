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

        // 以下の機能は無効化（相互マッチング機能のみ使用）
        /*
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
        */

        /// <summary>
        /// マッチング成立時に両方のユーザーに相互通知メールを送信します
        /// </summary>
        /// <param name="user1">ユーザー1のスキルデータ</param>
        /// <param name="user2">ユーザー2のスキルデータ</param>
        /// <returns></returns>
        public async Task<bool> SendMutualMatchNotificationAsync(SkillData user1, SkillData user2)
        {
            bool user1Success = false;
            bool user2Success = false;

            // ユーザー1にユーザー2の情報を送信
            if (!string.IsNullOrEmpty(user1.メールアドレス))
            {
                user1Success = await SendMatchEstablishedEmailAsync(user1, user2);
            }

            // ユーザー2にユーザー1の情報を送信
            if (!string.IsNullOrEmpty(user2.メールアドレス))
            {
                user2Success = await SendMatchEstablishedEmailAsync(user2, user1);
            }

            return user1Success || user2Success; // 少なくとも一方が成功すればtrueを返す
        }

        /// <summary>
        /// マッチング成立通知メールを送信します
        /// </summary>
        /// <param name="toUser">送信先ユーザー</param>
        /// <param name="matchedUser">マッチしたユーザー</param>
        /// <returns></returns>
        private async Task<bool> SendMatchEstablishedEmailAsync(SkillData toUser, SkillData matchedUser)
        {
            string subject = "【ShareSuki】スキルマッチング成立のお知らせ";
            
            string body = $@"
{toUser.氏名} 様

いつもShareSukiをご利用いただき、ありがとうございます。

✨ スキルマッチングが成立しました！ ✨

あなたが求めているスキル「{toUser.求スキル}」と、
{matchedUser.氏名}さんが提供可能なスキル「{matchedUser.譲スキル}」がマッチしました。

■マッチした相手の詳細情報：
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
お名前: {matchedUser.氏名}
学籍番号: {matchedUser.学籍番号}
クラス: {matchedUser.クラス名}
出席番号: {matchedUser.出席番号}
メールアドレス: {matchedUser.メールアドレス}

提供可能スキル: {matchedUser.譲スキル}
スキル詳細: {matchedUser.譲スキル備考}
対応可能期間: {matchedUser.待機期間}

相手が求めているスキル: {matchedUser.求スキル}
求スキル詳細: {matchedUser.求スキル備考}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📧 次のステップ：
1. 上記のメールアドレスに直接連絡を取ってください
2. お互いのスケジュールを調整してください
3. スキル交換の詳細（場所・時間・方法など）を決めてください

💡 コミュニケーションのポイント：
・最初の連絡では、ShareSukiでマッチしたことを伝えてください
・具体的な学習内容や教え方について事前に相談しましょう
・お互いのレベルに合わせた教え方を心がけましょう

素敵なスキル交換ができることを願っています！

---
ShareSuki運営チーム
技術支援部

※このメールに関するお問い合わせは、ShareSuki運営チームまでご連絡ください。
";

            return await SendEmailAsync(toUser.メールアドレス, toUser.氏名, subject, body);
        }
    }
}
