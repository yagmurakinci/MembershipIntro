using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipIntro_DataAccessLayer.ContextInfo;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using MembershipIntro_EntityLayer.Entities;
using MembershipIntro_EntityLayer.ViewModels;

namespace MembershipIntro_DataAccessLayer.ImplementationsOfRepo
{
    public class MemberPhoneRepository : Repository<MemberPhone, int>, IMemberPhoneRepository
    {
        public MemberPhoneRepository(MembershipIntroContext context)
            : base(context)
        {

        }
        //NOT: GetAll, GetByConditions, GetById yeterli gelmediği
        //durumda Repo'nun içine yeni metot oluşturup Context aracılığıyla işlemlerinizi gerçekleştirebilirsiniz.

        public List<string> Deneme()
        {
            //bugün 19 Ocak 2023
            // 09 Ocak Pzt --> 15 Ocak Pazar
            //Geçen haftanın pazartesi günününden geçen haftanın pazar  günü dahil sisteme eklenen türlere göre telefon sayıları 
            var today = DateTime.Now;
            DateTime startOfWeek = new DateTime();
            DateTime endOfWeek = new DateTime();
            var alldaysweek = Enum.GetValues(typeof(DayOfWeek));

            //Düşüneceğiz :D
            //for (int i = 0; i < alldaysweek.Length; i++)
            //{
            //    if ((int)DateTime.Now.DayOfWeek==i) //olduğum gün
            //    {
            //        endOfWeek = DateTime.Now.AddDays();
            //        startOfWeek = DateTime.Now.AddDays(i-7);
            //    }
            //}


            switch (today.DayOfWeek) //eğer bugün
            {
                //createdDate > 08 Ocak && createdDate < 16 Ocak
                case DayOfWeek.Sunday:  //15 Ocak
                    endOfWeek = DateTime.Now.AddDays(1);
                    startOfWeek = DateTime.Now.AddDays(-7);
                    break;
                //createdDate > 08 Ocak && createdDate < 16 Ocak
                case DayOfWeek.Monday: //16 Ocak olurdu
                    endOfWeek = DateTime.Now;
                    startOfWeek = DateTime.Now.AddDays(-8);
                    break;
                //createdDate > 08 Ocak && createdDate < 16 Ocak
                case DayOfWeek.Tuesday://17 Ocak olurdu
                    endOfWeek = DateTime.Now.AddDays(-1);
                    startOfWeek = DateTime.Now.AddDays(-9);
                    break;
                case DayOfWeek.Wednesday:
                    endOfWeek = DateTime.Now.AddDays(-2);
                    startOfWeek = DateTime.Now.AddDays(-10);
                    break;
                //createdDate > 08 Ocak && createdDate < 16 Ocak
                case DayOfWeek.Thursday: //19 ocak
                    endOfWeek = DateTime.Now.AddDays(-3);
                    startOfWeek = DateTime.Now.AddDays(-11);
                    break;
                case DayOfWeek.Friday:
                    endOfWeek = DateTime.Now.AddDays(-4);
                    startOfWeek = DateTime.Now.AddDays(-12);
                    break;
                case DayOfWeek.Saturday:
                    endOfWeek = DateTime.Now.AddDays(-5);
                    startOfWeek = DateTime.Now.AddDays(-13);
                    break;
                default:
                    break;
            }

            var data = from phonetype in _contex.PhoneTypeTable
                       join memberphone in _contex.MembersPhonesTable
                       on phonetype.Id equals memberphone.PhoneTypeId
                       where memberphone.CreatedDate > startOfWeek && memberphone.CreatedDate < endOfWeek &&
                       !memberphone.IsRemoved
                       select new { phonetype.Name };
            List<string> returnData = new List<string>();
            foreach (var item in data)
            {
                returnData.Add(item.Name);

            }
            return returnData;
        }

        public PhoneTypeQuantityLineChartVM GetPhoneTypeQuantityLineChart()
        {
            try
            {
                PhoneTypeQuantityLineChartVM linechartdata = new PhoneTypeQuantityLineChartVM();
                var today = DateTime.Now;
                DateTime startOfWeek = new DateTime();
                DateTime endOfWeek = new DateTime();
                var alldaysweek = Enum.GetValues(typeof(DayOfWeek));
                switch (today.DayOfWeek) //eğer bugün
                {
                    //createdDate > 08 Ocak && createdDate < 16 Ocak
                    case DayOfWeek.Sunday:  //15 Ocak
                        endOfWeek = DateTime.Now.AddDays(1);
                        startOfWeek = DateTime.Now.AddDays(-7);
                        break;
                    //createdDate > 08 Ocak && createdDate < 16 Ocak
                    case DayOfWeek.Monday: //16 Ocak olurdu
                        endOfWeek = DateTime.Now;
                        startOfWeek = DateTime.Now.AddDays(-8);
                        break;
                    //createdDate > 08 Ocak && createdDate < 16 Ocak
                    case DayOfWeek.Tuesday://17 Ocak olurdu
                        endOfWeek = DateTime.Now.AddDays(-1);
                        startOfWeek = DateTime.Now.AddDays(-9);
                        break;
                    case DayOfWeek.Wednesday:
                        endOfWeek = DateTime.Now.AddDays(-2);
                        startOfWeek = DateTime.Now.AddDays(-10);
                        break;
                    //createdDate > 08 Ocak && createdDate < 16 Ocak
                    case DayOfWeek.Thursday: //19 ocak
                        endOfWeek = DateTime.Now.AddDays(-3);
                        startOfWeek = DateTime.Now.AddDays(-11);
                        break;
                    //createdDate > 08 Ocak && createdDate < 16 Ocak --> 09 ocak 15 ocak almış oluyoruz
                    case DayOfWeek.Friday: //20 ocak 
                        endOfWeek = DateTime.Now.AddDays(-4);
                        startOfWeek = DateTime.Now.AddDays(-12);
                        break;
                    case DayOfWeek.Saturday:
                        endOfWeek = DateTime.Now.AddDays(-5);
                        startOfWeek = DateTime.Now.AddDays(-13);
                        break;
                    default:
                        break;
                }

                var data = from phonetype  //alias takma isim
                           in _contex.PhoneTypeTable
                           join memberphone //alias takma isim
                           in    _contex.MembersPhonesTable
                           on phonetype.Id equals memberphone.PhoneTypeId
                           where memberphone.CreatedDate > startOfWeek && memberphone.CreatedDate < endOfWeek &&
                           !memberphone.IsRemoved
                           select new { phonetype.Name, memberphone.CreatedDate };


                foreach (var item in data) // geçen hafta eklenen telefonlar ve türlerini tek tek dolaşalım
                {
                    if (linechartdata.PhoneTypeNameandQuantity.Keys.Count(x=> x==item.Name)==0) // tür/tip
                    {
                        linechartdata.PhoneTypeNameandQuantity.Add(item.Name,1); // Cep --->1 --> cep türünde 1 adet telefon var
                    }
                    else // Cep zaten ekliyse??
                    {
                      linechartdata.PhoneTypeNameandQuantity[item.Name]++;
                    }

                    var day = item.CreatedDate.DayOfWeek; //perşembe
                    if (linechartdata.DayofWeekandQuantity.Keys.Count(x=> x==day)==0)
                    {
                        linechartdata.DayofWeekandQuantity.Add(day, 1);
                    }
                    else
                    {
                        linechartdata.DayofWeekandQuantity[day]++;
                    }
                }
                return linechartdata;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
