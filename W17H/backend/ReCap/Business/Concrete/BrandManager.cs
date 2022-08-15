using Business.Abstarct;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Business.Constants;
using DataAccess.Abstract;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _ıBrandDal;

        public BrandManager(IBrandDal ıBrandDal)
        {
            _ıBrandDal = ıBrandDal;
        }
        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            _ıBrandDal.Add(brand);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Brand brand)
        {
            _ıBrandDal.Update(brand);
            _ıBrandDal.Add(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }


        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            _ıBrandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_ıBrandDal.GetAll(),Messages.BrandListed);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Brand> GetbyId(int id)
        {
            return new SuccessDataResult<Brand>(_ıBrandDal.Get(b => b.Id == id),Messages.BrandListed);
        }
        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            _ıBrandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}
