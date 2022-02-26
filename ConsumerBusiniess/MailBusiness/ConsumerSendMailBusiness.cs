using Data.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerBusiniess.MailBusiness
{
    public class ConsumerSendMailBusiness
    {
        public void SendMail(Mail mail)
        { 
            using MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mail.SenderEmail));
            email.To.Add(MailboxAddress.Parse(mail.ArriveEmail));
            email.Subject = mail.MailSubject;
            email.Body = new TextPart(TextFormat.Plain) { Text = mail.MailText };
            using SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("erhan.necati@gmail.com", "erhan41245642266hayret");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
