using System.Configuration;

namespace ShareSuki
{
    public static class EmailConfig
    {
        // Gmail SMTPサーバーの設定例（他のプロバイダーでも使用可能）
        public static string SmtpServer => ConfigurationManager.AppSettings["SmtpServer"] ?? "smtp.gmail.com";
        public static int SmtpPort => int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
        public static string FromEmail => ConfigurationManager.AppSettings["FromEmail"] ?? "";
        public static string FromName => ConfigurationManager.AppSettings["FromName"] ?? "ShareSuki";
        public static string Username => ConfigurationManager.AppSettings["EmailUsername"] ?? "";
        public static string Password => ConfigurationManager.AppSettings["EmailPassword"] ?? "";

        /// <summary>
        /// EmailServiceのインスタンスを作成します
        /// </summary>
        /// <returns></returns>
        public static EmailService CreateEmailService()
        {
            return new EmailService(SmtpServer, SmtpPort, FromEmail, FromName, Username, Password);
        }
    }
}
