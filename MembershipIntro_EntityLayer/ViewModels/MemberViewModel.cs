using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.ViewModels
{
    public class MemberViewModel
    {
        [StringLength(36, MinimumLength = 36,
   ErrorMessage = "Id PK old. için boş geçilemez!")]
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Kullanıcı adı gereklidir")]
        //araştırlacak --> küçük harfli a-z ve alt tire, tire
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public byte[] Salt { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İsim alanı gereklidir")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisim alanı gereklidir")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }
        public bool IsRemoved { get; set; }
        public string? ForgetPasswordToken { get; set; }
        public List<MemberPhoneViewModel>? MemberPhone { get; set; }
       
    }
}
