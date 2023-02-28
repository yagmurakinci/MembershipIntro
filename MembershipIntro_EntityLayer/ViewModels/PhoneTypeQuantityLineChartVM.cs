using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.ViewModels
{
    public class PhoneTypeQuantityLineChartVM
    {
        public Dictionary<string, int> PhoneTypeNameandQuantity { get; set; }
        public Dictionary<DayOfWeek, int> DayofWeekandQuantity { get; set; }
        public PhoneTypeQuantityLineChartVM()
        {
            PhoneTypeNameandQuantity = 
                new Dictionary<string, int>();
            DayofWeekandQuantity = 
                new Dictionary<DayOfWeek, int>();
            DayofWeekandQuantity.Add(DayOfWeek.Monday, 0);
            DayofWeekandQuantity.Add(DayOfWeek.Tuesday, 0);
            DayofWeekandQuantity.Add(DayOfWeek.Wednesday, 0);
            DayofWeekandQuantity.Add(DayOfWeek.Thursday, 0);
            DayofWeekandQuantity.Add(DayOfWeek.Friday, 0);
            DayofWeekandQuantity.Add(DayOfWeek.Saturday, 0);
            DayofWeekandQuantity.Add(DayOfWeek.Sunday, 0);
        }
    }
}
