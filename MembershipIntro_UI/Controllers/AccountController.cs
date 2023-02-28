using MembershipIntro_BusinessLayer.EmailSenderBusiness;
using MembershipIntro_BusinessLayer.Implementations;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_EntityLayer.ViewModels;
using MembershipIntro_UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace MembershipIntro_UI.Controllers
{
    public class AccountController : Controller
    {
        // interfaceler ile DI yapılacaktır
        private readonly IMemberService _memberService;
        private readonly IEmailSenderService _emailSenderService;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public AccountController(IMemberService memberService, IEmailSenderService emailSenderService)
        {
            _memberService = memberService;
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        public IActionResult Login(string? username) // httpget
        {
            // sayfada bir email/username 
            //bir de parola inputlarıyla tasarlanacak
            //ViewModel ya da DTO
            if (!string.IsNullOrEmpty(username))
            {
                LoginViewModel model = new LoginViewModel()
                {
                    Username = username
                };
                return View(model);
            }
            return View(); // kontrol edeceğiz
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Bilgileri eksiksiz girdiğinize emin olunuz");
                    return View(model);
                }
                var user = _memberService.GetByConditions(x =>
                x.UserName == model.Username).Data;
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve şifreyi doğru girdiğinizden emin olunuz!");
                    return View(model);
                }

                var passwordCompare = VerifyPassword(model.Password, user.Password, user.Salt);

                if (passwordCompare)
                {
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı ve şifrenizi doğru girdiğinize emin olunuz");
                    return View(model);

                }
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", "Beklenmedik bir sorun oluştu. Tekrar deneyiniz");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            // buraya geri döneceğim

            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                //gelen bilgiler tam mı? Kurallara uygun mu
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Bilgileri eksiksiz girdiğinize emin olunuz");
                    return View(model);
                }

                //1) Aynı kullanıcıdan adından varsa hata ver geri çevir
                var sameUser = _memberService.GetByConditions(x => x.UserName == model.UserName).Data;
                if (sameUser != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı sistemde vardır. Aynısını alamazsınız!");
                    return View(model);
                }
                MemberViewModel member = new MemberViewModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsRemoved = false,
                    Email = model.Email,
                    CreatedDate = DateTime.Now,
                    UserName = model.UserName,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    Id = Guid.NewGuid().ToString()
                };
                //todo: saltı kayıt etmek gerekecek mi?
                byte[] salt;
                member.Password = HashPasword(model.Password, out salt);
                member.Salt = salt;
                var memberResult = _memberService.Add(member);
                if (memberResult.IsSuccess)
                {
                    //Hoşgeldiniz emailini atacağız.
                    var emailMsg = new EmailMessage()
                    {
                        To = new string[] { member.Email },
                        Subject = "Wissen302-MembershipIntro - HOŞGELDİNİZ!",
                        Body = $"<html lang='tr'><head></head><body>" +
                        $"Merhaba Sayın {member.FirstName} {member.LastName}, <br/> Sisteme kaydınız gerçekleşmiştir.</body></html>"
                    };
                    _emailSenderService.SendEmailAsync(emailMsg);
                    //Login sayfasına giderken mesaj verelim
                    TempData["RegisterSuccessMsg"] = $"{member.FirstName} {member.LastName} kaydınız başarıyla gerçekleşti. Giriş yapabilirsiniz.";

                    return RedirectToAction("Login", "Account",
                        new { username = model.UserName });
                }
                else
                {
                    ModelState.AddModelError("", memberResult.Message);
                    return View(model);
                }
            }
            catch (Exception hata)
            {
                ViewBag.Error = "Beklenmedik bir sorun oluştu tekrar deneyiniz";
                //hata loglanacak
                return View(model);
            }
        }

        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(string username)
        {
            try
            {
                var user = _memberService.GetByConditions(x => x.UserName == username).Data;
                if (user == null)
                {
                    ViewBag.ForgetPasswordFailedMessage = "Girdiğiniz kullanıcı adına ait kaydımız bulunmamaktadır. Lütfen tekrar deneyiniz ya da kayıt olunuz!";
                    return View();
                }
                var token = Guid.NewGuid().ToString().Replace("-", "");
                var tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                user.ForgetPasswordToken = token;
                _memberService.Update(user);
                //www.localhost.com/Account/CreatePassword?userid=xxxx&token=xxxx
                var url = Url.Action("CreatePassword", "Account",
                    new
                    {
                        userid = user.Id,
                        token = tokenEncoded
                    }, protocol: Request.Scheme);
                var email = new EmailMessage()
                {
                    To = new string[] { user.Email },
                    Subject = $"Wissen302 - MembershipIntro Şifremi Unuttum!",
                    Body = $"Merhaba {user.FirstName} {user.LastName},<br/>" +
                    $"Yeni parolanızı belirlemek için <a href='{HtmlEncoder.Default.Encode(url)}' >buraya</a> tıklayınız."
                };
                if (_emailSenderService.SendEmail(email))
                {
                    ViewBag.ForgetPasswordSuccessMessage = "Şifre yenileme talebiniz sistemde kayıtlı emailinize gönderildi. Emailinizi kontrol ediniz";
                    return View();
                }
                else
                {
                    ViewBag.ForgetPasswordFailedMessage = "Şifre yenileme talebiniz malesef gerçekleşemedi. Tekrar deneyiniz.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ForgetPasswordFailedMessage = "Beklenmedik bir hata oluştu. Tekrar deneyiniz.";
                //ex loglanmalı
                return View();
            }
        }

        [HttpGet]
        public IActionResult CreatePassword(string userid, string token)
        {
            if (userid==null || token==null)
            {
                ModelState.AddModelError("","Kullanıcı bulunamadı tekrar deneyiniz");
                return View();
            }
            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                 UserId=userid,
                 Token=token
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreatePassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = _memberService.GetByConditions(x=> x.Id== model.UserId).Data;
                if (user == null)
                {
                    ModelState.AddModelError("","Kullanıcı bulunamadı! Tekrar deneyiniz!");
                    return View(model);
                }
                var tokenDecode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
               
                if (user.ForgetPasswordToken==tokenDecode)
                {
                    byte[] salt;
                    user.Password = HashPasword(model.Password, out salt);
                    user.Salt = salt;
                    user.ForgetPasswordToken = null;
                   if(_memberService.Update(user).IsSuccess)
                    {
                        TempData["ResetPasswordMessage"] = "Şifreniz başarılı bir şekilde yenilendi! Giriş yapabilirsiniz";
                        return RedirectToAction("Login", "Account", new { username=user.UserName});
                    }
                    else
                    {
                        ModelState.AddModelError("", "İşlem başarısız. Tekrar deneyiniz!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Linkin geçerlilik hakkı bittiği için işleminizi yapamıyorum. Tekrar şifremi unuttum sayfasına gidip, talep gönderiniz!");
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu! Tekrar deneyiniz");
                //ex loglanacak
                return View(model);
            }
        }

        [Authorize] // Authorize sisteme sing in olmadan bu sayfaya erişmeyi engelliyor.
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
