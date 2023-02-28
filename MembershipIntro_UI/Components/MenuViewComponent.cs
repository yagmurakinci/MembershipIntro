using MembershipIntro_BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MembershipIntro_UI.Components
{
    public class MenuViewComponent:ViewComponent
    { 
        //controllerdaki ctor işlemi burada ihtiyaç varsa yapılabilir
        //Yani IMemberService, IEmailSenderServici vb servislerinizi DI yapabilirsiniz.
        private readonly IMemberService _memberService;
        public MenuViewComponent(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IViewComponentResult Invoke()
        {
            string? userid = HttpContext.User.Identity?.Name;
            TempData["LoggedInUserNameSurname"] = null;
            if (userid!=null)
            {
                var user = _memberService.GetById(userid).Data;
                TempData["LoggedInUserNameSurname"] = $"{user.FirstName} {user.LastName}";
            }
            return View();
        }
    }
}
