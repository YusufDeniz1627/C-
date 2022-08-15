using Core.DataAccess;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalsDal: IEntityRepository<Rental>
    {
        List<RentalsDetailDto> GetRentalDetails();
    }
}
