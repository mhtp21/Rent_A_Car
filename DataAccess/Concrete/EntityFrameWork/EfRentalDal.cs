using Core.DataAccess.EntityFrameWork;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetCarDetails(Expression<Func<Rental,bool>> filter = null)
        {
            using(CarRentalContext context = new CarRentalContext())
            {
                var result = from r in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join c in context.Cars
                             on r.CarId equals c.Id
                             join cu in context.Customers
                             on r.CustomerId equals cu.UserId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join u in context.Users
                             on cu.UserId equals u.UserId
                             select new RentalDetailDto
                             {
                                 RentalId = r.Id,
                                 CarName = b.Name,
                                 CustomerName = u.FirstName + " " + u.LastName,
                                 UserName = u.FirstName + " " + u.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 TotalPrice = Convert.ToDecimal(r.ReturnDate.Date.Day - r.RentDate.Day) * c.DailyPrice

                             };
                return result.ToList();
            }
        }
    }
}