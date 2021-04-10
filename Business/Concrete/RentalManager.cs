using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var carRentalList = _rentalDal.GetAll(r => r.CarId == rental.CarId);
            foreach(var carRental in carRentalList)
            {
                if (carRental.ReturnDate == null || carRental.ReturnDate > DateTime.Now || carRental.ReturnDate > rental.RentDate)
                    return new ErrorResult("İstediğiniz araç kiralanmaz çünkü başka bir müşteride");
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Rental rental)
        {
            if(_rentalDal.Get(r=> r.Id == rental.Id) == null) { return new ErrorResult(Messages.RentalNotFound); }
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            var rental = _rentalDal.Get(r => r.Id == rentalId);
            if (rental == null)
            {
                return new ErrorDataResult<Rental>(Messages.RentalNotFound);
            }
            return new SuccessDataResult<Rental>(rental);
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            if (_rentalDal.Get(r => r.Id== rental.Id) == null)
            {
                return new ErrorResult(Messages.RentalNotFound);
            }
            _rentalDal.Update(rental);
            return new ErrorResult(Messages.RentalUpdated);
        }
    }
}
