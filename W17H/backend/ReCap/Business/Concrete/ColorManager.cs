using Business.Abstarct;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _ıColorDal;

        public ColorManager(IColorDal ıColorDal)
        {
            _ıColorDal = ıColorDal;
        }
        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
                _ıColorDal.Add(color);
                return new SuccessResult();
            
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Color color)
        {
            _ıColorDal.Update(color);
            _ıColorDal.Add(color);
            return new SuccessResult(Messages.ColorUpdated);
        }


        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            _ıColorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_ıColorDal.GetAll(),Messages.ColorListed);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_ıColorDal.Get(c=>c.Id == id),Messages.ColorListed);
        }
        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            _ıColorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}
