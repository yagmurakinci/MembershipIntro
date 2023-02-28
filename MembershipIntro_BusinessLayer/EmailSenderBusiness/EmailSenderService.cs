using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.EmailSenderBusiness
{
    public class EmailSenderService : IEmailSenderService
    {
        //NOT: Sender'ın email adresi appsettings.json'ın içinde olduğundan dolayı buraya appsetting.json'a ulaşacak IConfiguration isimli interfacei kullanmalıyız
        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //appsetting.json içindeki MembershipIntroEmailOptions bölümünün altındaki  SenderMail değerini al
        public string SenderMail => _configuration.GetSection("MembershipIntroEmailOptions:SenderMail").Value;

        public string Password => _configuration.GetSection("MembershipIntroEmailOptions:Password").Value;

        public string Smtp => _configuration.GetSection("MembershipIntroEmailOptions:Smtp").Value;
        public int SmtpPort => int.Parse(_configuration.GetSection("MembershipIntroEmailOptions:SmtpPort").Value);

        public string CCManagers => _configuration.GetSection("ProjeYoneticileri:Emailler").Value;


        public bool SendEmail(EmailMessage message)
        {
            try
            {
                MailMessage mail;
                SmtpClient smtpClient;
                MailInfoSet(message, out mail, out smtpClient);
                smtpClient.Send(mail);
                return true;

            }
            catch (Exception ex)
            {
                // ex loglanmalıdır!
                return false;
            }
        }

        private void MailInfoSet(EmailMessage message, out MailMessage mail, out SmtpClient smtpClient)
        {
            mail = new MailMessage()
            {
                From = new MailAddress(SenderMail) //wissen302sinifi emaili
            };
            // To emaili kime göndereceği
            foreach (var item in message.To)
            {
                mail.To.Add(item);
            }
            //cc
            if (message.CC != null)
            {
                foreach (var item in message.CC)
                {
                    mail.CC.Add(item);
                }
            }
            //appsettings içindeki proje yöneticilerini de cc ye ekleyelim
            if (CCManagers != null)
            {
                foreach (var item in CCManagers.Split(','))
                {
                    mail.CC.Add(item);
                }
            }
            //bcc
            if (message.BCC != null)
            {
                foreach (var item in message.BCC)
                {
                    mail.Bcc.Add(item);
                }
            }
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.HeadersEncoding = Encoding.UTF8;
            // artık mail gönderilecek
            smtpClient = new SmtpClient(Smtp, SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(SenderMail, Password)
            };
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            try
            {
                //Buradaki tek fark SendMail ASENKRON işlem yapar

                MailMessage mail;
                SmtpClient smtpClient;
                MailInfoSet(message, out mail, out smtpClient);
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                // ex loglansın

            }
        }
    }
}
