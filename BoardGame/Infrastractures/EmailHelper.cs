using System.Net.Mail;
using Utilities;

namespace BoardGame.Infrastractures
{
    public class EmailHelper
    {
        private readonly string AppName;
        private readonly string EmailAccount;
        private readonly string EmailPassword;

        public EmailHelper(IConfiguration configuration)
        {
            AppName = configuration["AppName"];
            EmailAccount = configuration["Email:Account"];
            EmailPassword = configuration["Email:Password"];
        }
        /// <summary>
        /// Sends a confirmation email to a newly registered user.
        /// </summary>
        /// <param name="confirmationUrl">The URL for the user to click to confirm their registration.</param>
        /// <param name="name">The name of the registered user.</param>
        /// <param name="emailAddress">The email address of the registered user.</param>
        public void SendConfirmRegisterEmail(string confirmationUrl, string name, string email)
        {
            string subject = "[New Member Confirmation Email]";
            string body = $@"Hi {name},

						<br />
                        Please click on this link [<a href='{confirmationUrl}' target='_blank'>Verify Email</a>] to complete your registration.
                        <br />
                        If you did not request this email, please ignore it. Thank you!

                        <br />
                        Sincerely,
                        The {AppName} Team";

            var mailMessage = new MailMessage(EmailAccount, email, subject, body)
            {
                IsBodyHtml = true
            };

            EmailUtility.SendEmailViaGmailSmtp(mailMessage, EmailAccount, EmailPassword);
        }
    }
}
