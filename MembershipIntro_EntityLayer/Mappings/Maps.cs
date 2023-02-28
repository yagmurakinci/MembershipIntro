using AutoMapper;
using MembershipIntro_EntityLayer.Entities;
using MembershipIntro_EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_EntityLayer.Mappings
{
    public class Maps:Profile
    {
        // Kim kime dönüşşsün?
        public Maps()
        {
            CreateMap<Member, MemberViewModel>();
            CreateMap<MemberViewModel, Member>(); // bu satırı yazmayalım diye bu satırın işini yapacak REVERSEMAP kullanabilir.

            CreateMap<PhoneType, PhoneTypeViewModel>().ReverseMap();

            //CreateMap<PhoneType, PhoneTypeViewModel>()
            //CreateMap<PhoneTypeViewModel, PhoneType>();
            CreateMap<MemberPhone, MemberPhoneViewModel>().ReverseMap();
        }
    }
}
