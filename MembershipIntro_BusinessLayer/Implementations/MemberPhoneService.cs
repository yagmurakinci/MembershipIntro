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
    public class MemberPhoneService:Service<MemberPhoneViewModel,MemberPhone,int>,
        IMemberPhoneService
    {
        private readonly IMemberPhoneRepository _memberphonerepo;
        public MemberPhoneService(IMapper mapper,
            IMemberPhoneRepository repo,
            IMemberPhoneRepository memberphonerepo)
            : base(repo, mapper, "PhoneType,Member")
        {
            _memberphonerepo = memberphonerepo;
        }

        public List<string> DenemeBetul()
        {
          return  _memberphonerepo.Deneme();
        }

        public PhoneTypeQuantityLineChartVM GetPhoneTypeQuantityLineChart()
        {
            try
            {
                return _memberphonerepo.GetPhoneTypeQuantityLineChart();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
