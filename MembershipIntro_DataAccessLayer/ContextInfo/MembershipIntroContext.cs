using MembershipIntro_EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_DataAccessLayer.ContextInfo
{
    public class MembershipIntroContext:DbContext
    {
        // Code-First yaptığımız için DbContext kalıtım aldık
        //Bu classın içinde CTOR aracılığıyla Connection string cümlesinin yeri belirtilir.
        //appsetting.json dosyasına Connection string cümlesi eklenir
        //Eklenen cümleye ulaşabilmek için Programçcs içine ayar kodları eklenir.

        public MembershipIntroContext(DbContextOptions<MembershipIntroContext> options)
            : base(options) 
        {
            //ctora parameter verdik. Generic bir parametre verdik. Böylece connectionstring özelliğimizi OPSİYON olaral Program.cs üzerinden ayarlayacağız.
            
        } // ctor bitti

        // tabloların DBSET propertylerini yazmamız gerekiyor.
        public DbSet<Member> MemberTable { get; set; }
        public DbSet<MemberPhone> MembersPhonesTable { get; set; }
        public DbSet<PhoneType> PhoneTypeTable { get; set; }
    }
}
