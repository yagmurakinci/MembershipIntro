using MembershipIntro_EntityLayer.Entities;
using MembershipIntro_EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_DataAccessLayer.InterfacesOfRepo
{
   public interface IMemberPhoneRepository:IRepository<MemberPhone,int>
    {
        List<string> Deneme(); // bu silinecek
        PhoneTypeQuantityLineChartVM GetPhoneTypeQuantityLineChart();
    }
}
