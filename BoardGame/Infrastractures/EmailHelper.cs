using System.Net.Mail;
using Utility;

namespace BoardGame.Infrastractures
{
    public class EmailHelper(IConfiguration configuration)
    {
        private readonly string AppName = configuration["AppName"] ?? string.Empty;
        private readonly string EmailAccount = configuration["Email:Account"] ?? string.Empty;
        private readonly string EmailPassword = configuration["Email:Password"] ?? string.Empty;
        private string Subject { get => "[New Member Confirmation Email]"; }

        /// <summary>
        /// Sends a confirmation email to a newly registered user.
        /// </summary>
        /// <param name="confirmationUrl">The URL for the user to click to confirm their registration.</param>
        /// <param name="name">The name of the registered user.</param>
        /// <param name="emailAddress">The email address of the registered user.</param>
        public void SendConfirmationEmail(string confirmationUrl, string name, string email)
        {
            
            string body = $@"Hi {name},

						<br />
                        Please click on this link [<a href='{confirmationUrl}' target='_blank'>Verify Email</a>] to activate your account.
                        <br />
                        If you did not request this email, please ignore it. Thank you!

                        <br />
                        Sincerely,
                        The {AppName} Team";

            var mailMessage = new MailMessage(EmailAccount, email, Subject, body)
            {
                IsBodyHtml = true
            };

            EmailUtility.SendEmailViaGmailSmtp(mailMessage, EmailAccount, EmailPassword);
        }
    }
}
