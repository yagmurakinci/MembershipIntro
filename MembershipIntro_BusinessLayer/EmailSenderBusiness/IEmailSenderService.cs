using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.EmailSenderBusiness
{
    public interface IEmailSenderService
    {
        bool SendEmail(EmailMessage message);
        Task SendEmailAsync(EmailMessage message);
    }
}
