using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MembershipIntro_DataAccessLayer.ContextInfo;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using Microsoft.EntityFrameworkCore;

namespace MembershipIntro_DataAccessLayer.ImplementationsOfRepo
{
    public class Repository<T, Id> : IRepository<T, Id>
        where T : class, new()
    {
        //dependecy injection SOLID "D" Depencey Inversion
        protected readonly MembershipIntroContext _contex;
        public Repository(MembershipIntroContext contex)
        {
            _contex = contex; //ctor DI
        }

        public bool Add(T entity)
        {
            try
            {
                //akbilYonetimi.Talimatlar.Add(yeniTalimat);
                _contex.Set<T>().Add(entity);
                return _contex.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _contex.Set<T>().Remove(entity);
                return _contex.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeRelationalTables = null)
        {
            try
            {
                IQueryable<T> query = _contex.Set<T>(); //select * from TabloAdi 
                if (filter!=null)
                {
                    //select * from TabloAdi where kosullar
                    query = query.Where(filter);
                }

                if (!string.IsNullOrEmpty(includeRelationalTables))
                {
                    //ilişkiliTabloAdi1,İlişkiliTabloAdi2,
                    //İlişkiliTabloAdi3
                    foreach (var item in includeRelationalTables.Split(","))
                    {
                        ////select * from TabloAdi t
                        //join ilişkiliTabloAdi1 i1 on t.Id=i1.xid
                        //join  ilişkiliTabloAdi2 i2 on t.Id=i2.xid
                        query = query.Include(item);
                    } // foreach bitt
                } // if bitt
                return query.AsNoTracking();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T GetByConditions(Expression<Func<T, bool>> filter = null, string includeRelationalTables = null)
        {
            try
            {
                IQueryable<T> query = _contex.Set<T>(); //select * from TabloAdi 
                if (filter != null)
                {
                    //select * from TabloAdi where kosullar
                    query = query.Where(filter);
                }

                if (!string.IsNullOrEmpty(includeRelationalTables))
                {
                    //ilişkiliTabloAdi1,İlişkiliTabloAdi2,
                    //İlişkiliTabloAdi3
                    foreach (var item in includeRelationalTables.Split(","))
                    {
                        ////select * from TabloAdi t
                        //join ilişkiliTabloAdi1 i1 on t.Id=i1.xid
                        //join  ilişkiliTabloAdi2 i2 on t.Id=i2.xid
                        query = query.Include(item);
                    } // foreach bitt
                } // if bitt
                return query.AsNoTracking().FirstOrDefault(); // bir tane entity döner

            }
            catch (Exception)
            {

                throw;
            }
        }

        public T GetById(Id id)
        {
            try
            {
                return _contex.Set<T>().Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                _contex.Set<T>().Update(entity);
                return _contex.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
