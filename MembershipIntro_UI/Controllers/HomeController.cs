using MembershipIntro_BusinessLayer.EmailSenderBusiness;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_EntityLayer.ViewModels;
using MembershipIntro_UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MembershipIntro_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPhoneTypeService _phoneTypeService;
        private readonly IMemberPhoneService _memberPhoneService;
        private readonly IEmailSenderService _emailSenderService;

        public HomeController(ILogger<HomeController> logger, IPhoneTypeService phoneTypeService, IMemberPhoneService memberPhoneService, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _phoneTypeService = phoneTypeService;
            _memberPhoneService = memberPhoneService;
            _emailSenderService = emailSenderService;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity?.Name != null)
            {
                string memberId = HttpContext.User.Identity?.Name;
                var data = _memberPhoneService
                               .GetAll(x => !x.IsRemoved
                               && x.MemberId == memberId).Data;

                return View(data);
            }
            else
            {
                return View();
            }

        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddPhone()
        {
            ViewBag.PhoneTypes = _phoneTypeService.GetAll(x => !x.IsRemoved).Data;
            MemberPhoneViewModel model = new MemberPhoneViewModel()
            {
                MemberId=HttpContext.User.Identity.Name
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPhone(MemberPhoneViewModel model)
        {
            try 
            {
                ViewBag.PhoneTypes = _phoneTypeService.GetAll(x => !x.IsRemoved).Data;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string memberid = HttpContext.User.Identity.Name;
                var isSamePhone = _memberPhoneService.GetByConditions(x=> x.MemberId==memberid && x.Phone==model.Phone).Data;
                if (isSamePhone!=null)
                {
                    ModelState.AddModelError("",$"Bu telefon {isSamePhone.PhoneType.Name} türünde zaten eklenmiştir!");
                    return View(model);
                }

                if (model.PhoneTypeId == 0)
                {
                    // Eğer phonetypeid 0 gelirse 
                    // Bu kişi combodaki türlerden seçmedi ya da henüz hiç tür olmadığı için seçemedi.
                    // Bu nedenle biz telefon türünü eklemeliyiz
                    //Ama telefon türü zaten varsa hata verecek
                    var isSamePhoneType = _phoneTypeService.GetByConditions(x => x.Name.ToLower() == model.AnotherPhoneTypeName.ToLower()).Data;
                    if (isSamePhoneType != null)
                    {
                        ModelState.AddModelError("", $"Bu tip/tür  zaten eklenmiştir!");
                        return View(model);
                    }

                    PhoneTypeViewModel phoneType = new PhoneTypeViewModel()
                    {
                        CreatedDate = DateTime.Now,
                        IsRemoved = false,
                        Name = model.AnotherPhoneTypeName
                    };
                    var resultPhoneType = _phoneTypeService.Add(phoneType).Data;
                    model.PhoneTypeId = resultPhoneType.Id; 
                } //if bitti
              
                model.MemberId = HttpContext.User.Identity.Name;
                model.CreatedDate = DateTime.Now;
               if( _memberPhoneService.Add(model).IsSuccess)
                {
                    TempData["AddPhoneMessage"]= "Yeni telefon eklendi";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Telefon ekleme başarısız!Tekrar deneyiniz!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Beklenmedik bir hata oluştu!");
                return View(model);
            }
        }
                [HttpPost]
        public JsonResult DeletePhone([FromBody]int id)
        {
            try
            {
                if (id > 0)
                {
                    var data = _memberPhoneService.GetByConditions(x=> x.Id== id).Data;
                    if (data == null)
                    {
                        return Json(new { isSuccess = false, message = "Kayıt bulunamadı! Silme işlemi başarısızdır!" });
                    }

                    data.IsRemoved = true; //soft delete
                    if( _memberPhoneService.Update(data).IsSuccess)
                    {
                        var phones = _memberPhoneService.GetAll(x=> 
                        !x.IsRemoved).Data;
                        return Json(new
                        {
                            isSuccess = true,
                            message = "Kayıt silindi!",
                            data=phones
                        });
                    }
                    else
                    {
                        return Json(new { isSuccess = false,
                            message = "Silme işlemi başarısızdır! Lütfen daha fazla tekrar deneyiniz!" });
                    }
                }
                else
                {
                    return Json(new { isSuccess = false, message = "Kayıt bulunamadı! Silme işlemi başarısızdır!" });
                }
            }
            catch (Exception ex)
            {
                // ex loglanacak
                return Json(new { isSuccess = false, message = "Beklenmedik bir hata oluştu!" });

            }
        }
    }

    }
  
