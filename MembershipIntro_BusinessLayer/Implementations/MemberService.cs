using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipIntro_EntityLayer.Entities;
using MembershipIntro_EntityLayer.ViewModels;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using AutoMapper;

namespace MembershipIntro_BusinessLayer.Implementations
{
    public class MemberService:Service<MemberViewModel,Member,string>,
        IMemberService
    {
        public MemberService(IMemberRepository repo, IMapper mapper)
            : base(repo, mapper, null)
        {

        }
    }
}
