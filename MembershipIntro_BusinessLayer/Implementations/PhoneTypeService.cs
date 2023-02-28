using AutoMapper;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using MembershipIntro_EntityLayer.Entities;
using MembershipIntro_EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.Implementations
{
    public class PhoneTypeService:Service<PhoneTypeViewModel, PhoneType,byte>, IPhoneTypeService
    {
        public PhoneTypeService(IMapper mapper, IPhoneTypeRepository repo)
            :base(repo,mapper,null)
        {

        }
    }
}
