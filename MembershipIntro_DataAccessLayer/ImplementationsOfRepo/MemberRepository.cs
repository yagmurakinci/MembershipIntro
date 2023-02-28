using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using MembershipIntro_EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipIntro_DataAccessLayer.ContextInfo;
namespace MembershipIntro_DataAccessLayer.ImplementationsOfRepo
{
    public class MemberRepository:Repository<Member,string>, IMemberRepository
    {
        public MemberRepository(MembershipIntroContext context)
            :base(context)
        {
            // Atası nasıl bir ctor ile yaratılyorsa burada o ctor olmalı ya da o ctora BASE ile gerekli parametre gönderilmeli

        }
    }
}
