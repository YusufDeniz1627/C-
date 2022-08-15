using Business.Abstarct;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalsManager : IRentalsService
    {
        IRentalsDal _ıRentalsDal;

        public RentalsManager(IRentalsDal ıRentalsDal)
        {
            _ıRentalsDal = ıRentalsDal;
        }
        [CacheRemoveAspect("IRentalsService")]
        [ValidationAspect(typeof(RentalsValidator))]
        [SecuredOperation("options,admin")]
        public IResult Add(Rental rentals)
        {
            var result = _ıRentalsDal.Get(r=>r.CarId==rentals.CarId && r.ReturnDate==null);
            if (result == null)
            {
                _ıRentalsDal.Add(rentals);
                return new SuccessResult(Messages.RentalsAdded);
            }
            return new ErrorResult(Messages.RentalsNotAdded);
               
        }
        [CacheRemoveAspect("IRentalsService")]
        [ValidationAspect(typeof(RentalsValidator))]
        [SecuredOperation("options,admin")]
        public IResult Deleted(Rental rentals)
        {
            _ıRentalsDal.Delete(rentals);
            return new SuccessResult(Messages.RentalsDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_ıRentalsDal.GetAll());
        }

        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_ıRentalsDal.Get(r=>r.Id==Id));
        }

        public IDataResult<List<RentalsDetailDto>> GetRentalDetails()
        {
            var result = _ıRentalsDal.GetRentalDetails();
            if (result != null)
            {
                
                return new SuccessDataResult<List<RentalsDetailDto>>(_ıRentalsDal.GetRentalDetails(), "Araç detaylarıe getirildi");
            }
            return new ErrorDataResult<List<RentalsDetailDto>>("hata");
            
        }

        [SecuredOperation("options,admin")]
        public IResult Updated(Rental rentals)
        {
            _ıRentalsDal.Update(rentals);
            return new SuccessResult(Messages.RentalsUpdated);
        }
    }
}
