using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.EmailSenderBusiness
{
    public class EmailMessage
    {
        public string[] To { get; set; }//kimlere gidecek
        public string[] CC { get; set; }
        public string[] BCC { get; set; }
        public string Subject { get; set; } //Mail konu başlığı
        public string Body { get; set; } //Mail içerik
    }
}
