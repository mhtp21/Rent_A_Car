using System;
using Core.DataAccess.EntityFrameWork;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Text;
using DataAccess.Concrete.EntityFramework;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfCarImageDal: EfEntityRepositoryBase<CarImage, CarRentalContext>, ICarImageDal
    {
    }
}
