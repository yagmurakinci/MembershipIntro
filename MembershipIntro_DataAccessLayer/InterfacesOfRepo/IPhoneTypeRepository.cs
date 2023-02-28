using MembershipIntro_EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_DataAccessLayer.InterfacesOfRepo
{
    public interface IPhoneTypeRepository:IRepository<PhoneType,byte>
    {
    }
}
