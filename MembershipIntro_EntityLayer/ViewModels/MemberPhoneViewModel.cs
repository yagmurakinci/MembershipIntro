using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.ViewModels
{
    public class MemberPhoneViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRemoved { get; set; }

        [StringLength(17, MinimumLength = 17,
           ErrorMessage = "Telefon numarası 17 karakter olmalıdır.ÖRNEK: 0 (XXX) XXX YY ZZ ")]
        [RegularExpression(@"^0\s\((\d{3})\)\s\d{3}\s\d{2}\s\d{2}", ErrorMessage = "Yanlış format girdiniz. ÖRNEK: 0 (XXX) XXX YY ZZ")]
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public byte PhoneTypeId { get; set; }
        //viewmodeli sayfamda istediğim gibi hareket edebilmek için kullanıyorum aynı zamanda viewmodel ile veritabanındaki tablonun kolonlarını alabilir.
        public string? AnotherPhoneTypeName { get; set; }
        public PhoneTypeViewModel? PhoneType { get; set; }
    }
}
