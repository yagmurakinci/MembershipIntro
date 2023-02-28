using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MembershipIntro_BusinessLayer.Interfaces;
using MembershipIntro_DataAccessLayer.InterfacesOfRepo;
using MembershipIntro_EntityLayer.ResultModels;

namespace MembershipIntro_BusinessLayer.Implementations
{
    public class Service<TViewModel, TModel, Id> : IService<TViewModel, Id>
        where TViewModel : class, new()
        where TModel : class, new()
    {
        protected readonly IRepository<TModel, Id> _repo;
        private readonly IMapper _mapper;
        private readonly string _includeRelationalTables;

        public Service(IRepository<TModel, Id> repo, IMapper mapper, string includeRelationalTables)
        {
            _repo = repo;
            _mapper = mapper;
            _includeRelationalTables = includeRelationalTables;
        }
        public IDataResult<TViewModel> Add(TViewModel model)
        {
            try
            {
                //bize parametreden gelen model TViewModel
                TModel tmodel = _mapper.Map<TViewModel, TModel>(model);
                bool result = _repo.Add(tmodel); // ekleme yapıldı. TModel'in Idsi oluştu.
                TViewModel dataModel = _mapper.Map<TModel, TViewModel>(tmodel);
                // turnery if
                return result ?
                    new DataResult<TViewModel>(success: true, data: dataModel, message: $"Ekleme işlemi başarılıdır")
                    :
                     new DataResult<TViewModel>(success: false, data: null, message: $"Ekleme işlemi GERÇEKLEŞMEDİ!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IResult Delete(TViewModel model)
        {
            try
            {
                TModel tmodel = _mapper.Map<TViewModel, TModel>(model);
                bool deleteResult = _repo.Delete(tmodel);
                if (deleteResult)
                {
                    return new Result(true, "Silme işlemi başarılıdır!");
                }
                else
                {
                    return new Result(false, "Silme işlemi GERÇEKLEŞMEDİ!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IDataResult<ICollection<TViewModel>> GetAll(Expression<Func<TViewModel, bool>> filter = null)
        {
            try
            {
                var fltr = _mapper.Map<Expression<Func<TViewModel, bool>>, Expression<Func<TModel, bool>>>(filter);
                var data = _repo.GetAll(fltr, _includeRelationalTables);
                ICollection<TViewModel> dataList =
                    _mapper.Map<IQueryable<TModel>, ICollection<TViewModel>>(data);

                return new DataResult<ICollection<TViewModel>>(success: true, data: dataList, message: $"{dataList.Count} adet kayıt gönderildi.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IDataResult<TViewModel> GetByConditions(Expression<Func<TViewModel, bool>> filter = null)
        {
            try
            {
                var fltr = _mapper.Map<Expression<Func<TViewModel, bool>>,
                    Expression<Func<TModel, bool>>>(filter);
                var data = _repo.GetByConditions(fltr, _includeRelationalTables);
                if (data == null)
                {
                    return new DataResult<TViewModel>(null, "Kayıt bulunamadı", false);
                }
                var returnData = _mapper.Map<TModel, TViewModel>(data);
                return new DataResult<TViewModel>(returnData, "Kayıt bulundu", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<TViewModel> GetById(Id id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("id null geldiği için veriyi bulamıyorum!");
                }
                var data = _repo.GetById(id);
                if (data == null)
                {
                    throw new Exception("Kayıt bulunamadı!");
                }
                var returnData = _mapper.Map<TModel, TViewModel>(data);
                return new DataResult<TViewModel>(data: returnData, success: true);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IResult Update(TViewModel model)
        {
            try
            {
                TModel tmodel = _mapper.Map<TViewModel, TModel>(model);
                bool updateResult = _repo.Update(tmodel);
                if (updateResult)
                {
                    return new Result(true, "Güncelleme işlemi başarılıdır!");
                }
                else
                {
                    return new Result(false, "Güncelleme işlemi GERÇEKLEŞMEDİ!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
