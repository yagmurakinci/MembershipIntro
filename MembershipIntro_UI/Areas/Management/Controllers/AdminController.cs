using Microsoft.AspNetCore.Mvc;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_UI.Areas.Management.Models;
using MembershipIntro_EntityLayer.ViewModels;

namespace MembershipIntro_UI.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("m/[Controller]/[Action]/{id?}")] // Route sadece area için değildir. HomeController ve AccountController için de kullabılabilir.
    public class AdminController : Controller
    {
        public readonly IPhoneTypeService _phoneTypeService;
        public readonly IMemberPhoneService _memberPhoneService;

        public AdminController(IPhoneTypeService phoneTypeService, IMemberPhoneService memberPhoneService)
        {
            _phoneTypeService = phoneTypeService;
            _memberPhoneService = memberPhoneService;
        }
        public IActionResult Dashboard()
        {
            List<PhoneTypeCountModel> model = new List<PhoneTypeCountModel>();
            try
            {
                var phonetypes = _phoneTypeService.GetAll(x => !x.IsRemoved).Data; //cep ev enerji 777

                foreach (var item in phonetypes)
                {
                    PhoneTypeCountModel p = new PhoneTypeCountModel()
                    {
                        Name = item.Name
                    };
                    p.Quantity = _memberPhoneService.GetAll(x => !x.IsRemoved && x.PhoneTypeId == item.Id).Data.Count;
                    model.Add(p);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.DashboardErrorMessage = $"Beklenmedik bir hata oluştu!";
                return View(model);
            }
        }
        public IActionResult Graphics()
        {
            try
            {
                var data = _memberPhoneService.GetPhoneTypeQuantityLineChart();
                return View(data);
            }
            catch (Exception ex)
            {
                //ex loglanacak
                return View(new PhoneTypeQuantityLineChartVM());
            }
        }
        public JsonResult GetDays()
        {
            var data = new string[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };
            return Json(new
            {
                isSuccess = true,
                message = "Günler geldi!",
                data = data
            });
        }
        public JsonResult GetPoints()
        {
            try
            {
                var data = _memberPhoneService.GetPhoneTypeQuantityLineChart();
                int[] points = new int[7];
                var allPoints = data.DayofWeekandQuantity.Values;
                int sayac = 0;
                foreach (var item in allPoints)
                {
                    points[sayac] = item;
                    sayac++;
                }
                return Json(new
                {
                    isSuccess = true,
                    message = "Veriler geldi!",
                    data = points
                });
            }
            catch (Exception ex)
            {
                // ex loglanacak
                return Json(new
                {
                    isSuccess = false,
                    message = "Veriler gelmedi!",
                    data = new int[7]
                });
            }
        }

    }
}
