using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_DataAccessLayer.InterfacesOfRepo
{
    public interface IRepository<T,Id> where T: class, new()
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        // ÖRN: select * from kitaplar includeRelationalTables--> Yazarlar Turler işin içine kat !!!---> JOIN YAP 
        //select * from Kitaplar k join Yazarlar k.YazarId=y.Id
        //join Turler t on t.Id=k.TurId
        // includeRelationalTables ---> Yazarlar ve Turler
        IQueryable<T> GetAll(Expression<Func<T,bool>> filter=null,string includeRelationalTables=null);
        T GetById(Id id);
        T GetByConditions(Expression<Func<T, bool>> filter = null, string includeRelationalTables = null);
    }
}
