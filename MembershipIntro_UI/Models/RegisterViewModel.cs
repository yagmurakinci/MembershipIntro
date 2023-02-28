using System.ComponentModel.DataAnnotations;

namespace MembershipIntro_UI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad Alanı gereklidir!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Min 2 Mak 50 karakter olmalıdır")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad Alanı gereklidir!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Min 2 Mak 50 karakter olmalıdır")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Kullanıcı adı gereklidir!")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Min 2 Mak 32 karakter")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8,MinimumLength =8,ErrorMessage ="Parola 8 karakter olmalıdır")]
        [RegularExpression(@"^[a-z][a-z0-9_-]*$", ErrorMessage = @"Parola küçük harf ile başlamalıdır.Sonrasında küçük harf,rakam, tire ya da alt tire kullanılabilir. ")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor!")]
        public string ConfirmPassword { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }


    }
}
