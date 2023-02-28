using MembershipIntro_EntityLayer.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MembershipIntro_BusinessLayer.Interfaces
{
    public interface IService<T,Id>
    {
        //Ekleme, Güncelleme, silme, Listeleme, Idye göre bulma 
        //işlemlerini BAĞIMSIZLAŞMIŞ şekilde yapacak
        // Generic olması 
        IDataResult<T> Add(T model);
        IResult Update(T model);
        IResult Delete(T model);
        //expression
        // x=> x.SilindiMi==false && x.SayfaSayisi>400
        IDataResult<ICollection<T>> GetAll(Expression<Func<T,bool>> filter=null);
        IDataResult<T> GetById(Id id);
        IDataResult<T> GetByConditions(Expression<Func<T,bool>> filter=null);

    }
}
