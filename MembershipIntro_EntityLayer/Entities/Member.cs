using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.Entities
{
    [Table("Members")]
    public class Member
    {
        [Key]
        [Column(Order=1)] // 1 numaralı kolon
        [StringLength(36,MinimumLength =36,
            ErrorMessage ="Id PK old. için boş geçilemez!")]
        public string Id { get; set; }

        [Column(Order = 2)] // 2 numaralı kolon
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(32, MinimumLength =2, ErrorMessage ="Kullanıcı adı gereklidir")]
       //araştırlacak --> küçük harfli a-z ve alt tire, tire
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public byte[] Salt { get; set; }

        [Required]
        [StringLength(50,MinimumLength =2, ErrorMessage =   "İsim alanı gereklidir")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisim alanı gereklidir")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }
        public bool IsRemoved { get; set; }
        public string? ForgetPasswordToken { get; set; }
        //Eğer bu tablonun başka bir tablo ile ilişkisi olsaydı En altta VIRTUAL property tanımlardık.


        // Bakın bu classda yazmadık ve yine sorunsuz FK olacak
        //public virtual ICollection<MemberPhone> MemberPhones { get; set; }
    }
}
