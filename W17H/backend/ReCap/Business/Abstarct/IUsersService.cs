using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstarct
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int Id);
        List<OperationClaim> GetClaims(User user);
        User GetbyMail(string email);


        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
    }
}
