using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MongoIdentitySample.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //create the mail message
            MailMessage mail = new MailMessage
            {

                //set the addresses
                From = new MailAddress("me@mycompany.com")
            };
            mail.To.Add(email);

            //set the content
            mail.Subject = subject;
            mail.Body = message;

            //==== Check if directory exists or not. If not then create new directory/folder.
            DirectoryInfo dirInfo = new DirectoryInfo("C:\\TestEmails");
            if (!dirInfo.Exists)
            {
                Directory.CreateDirectory("C:\\TestEmails"); //=== Create new folder
            }

            SmtpClient smtp = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = "C:\\TestEmails"
            };
            return  smtp.SendMailAsync(mail);
        }
    }
}
