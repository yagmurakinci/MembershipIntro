using MembershipIntro_EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.Interfaces
{
    public interface IMemberService:IService<MemberViewModel,string>
    {
        // Viewmodel kullandığımız için Member' MemberViewModele
        //MemberViewmodeli Membera dönüştürmeye ihtiyacımız var
        //Bunu kendimiz yapabiliriz
        // ama işimiz daha kısa sürsün diye Mapster ya da AutoMapper paketinden yardım alacağız

    }
}
