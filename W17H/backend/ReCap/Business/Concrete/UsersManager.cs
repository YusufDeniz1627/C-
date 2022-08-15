using Business.Abstarct;
using Business.Constants;using Core.Entities.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.DTOs;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _ıUsersDal;

        public UserManager(IUserDal ıUserDal)
        {
            _ıUsersDal = ıUserDal;
        }

        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(UsersValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            _ıUsersDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }
        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(UsersValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            _ıUsersDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
        [CacheAspect]
        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<User>>(_ıUsersDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int Id)
        {
            return new SuccessDataResult<User>(_ıUsersDal.Get(s => s.Id == Id));
        }

        public User GetbyMail(string email)
        {
            return _ıUsersDal.Get(u => u.Email==email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _ıUsersDal.GetClaims(user);
        }
        [SecuredOperation("options,admin")]
        [ValidationAspect(typeof(UsersValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            _ıUsersDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

    }
}
