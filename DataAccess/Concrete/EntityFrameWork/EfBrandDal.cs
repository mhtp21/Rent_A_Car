 using Core.DataAccess.EntityFrameWork;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Text;
namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfBrandDal: EfEntityRepositoryBase<Brand, CarRentalContext>, IBrandDal
    {
    }
}
