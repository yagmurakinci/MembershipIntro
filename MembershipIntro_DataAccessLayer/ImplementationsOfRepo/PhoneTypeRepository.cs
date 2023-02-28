using MembershipIntro_EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using MembershipIntro_DataAccessLayer.ContextInfo;
namespace MembershipIntro_DataAccessLayer.ImplementationsOfRepo
{
    public class PhoneTypeRepository : Repository<PhoneType, byte>, IPhoneTypeRepository
    {
        public PhoneTypeRepository(MembershipIntroContext context)
            :base(context)
        {

        }
    }
   
}
