using MembershipIntro_EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.Interfaces
{
    public interface IMemberPhoneService: IService<MemberPhoneViewModel,int>
    {
        List<string> DenemeBetul(); // bu silinecektir.
        PhoneTypeQuantityLineChartVM GetPhoneTypeQuantityLineChart();
        

    }
}
