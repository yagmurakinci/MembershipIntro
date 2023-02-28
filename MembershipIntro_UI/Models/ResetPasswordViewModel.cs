using System.ComponentModel.DataAnnotations;

namespace MembershipIntro_UI.Models
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        [DataType(DataType.Password)]
        [StringLength(8,MinimumLength =8, ErrorMessage ="Şifre 8 karaker olmak zorundadır!")]
        [RegularExpression(@"^[a-z][a-z0-9_-]*$",ErrorMessage =@"Parolanız küçük harf ile başlamalı sonrasında küçük harf, rakam, tire ya da alt tire kullanabilirsiniz.")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
