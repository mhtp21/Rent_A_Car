using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;
using Core.Utilities.Concrete;
using Core.Utilities.Abstract;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _cardal;
        IColorService _colorService;
        IBrandService _brandService;

        public CarManager(ICarDal carDal,IColorService colorService, IBrandService brandService)
        {
            _cardal = carDal;
            _colorService = colorService;
            _brandService = brandService;
        }
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinesRules.Run(CheckIdBrandExists(car.BrandId), CheckIfColorExists(car.ColorId));
            if(result != null)
            {
                return result;
            }
            _cardal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            if(_cardal.Get(c=> c.Id == car.Id) == null) { return new ErrorResult(Messages.CarNotFound); }
            _cardal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
            
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_cardal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetByBrandId(int id)
        {
            var result = _cardal.GetAll(c => c.BrandId == id);
            if(result == null)
            {
                return new ErrorDataResult<List<Car>>(Messages.BrandNotFound);
            }
            return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetByColorId(int id)
        {
            var result = _cardal.GetAll(c => c.ColorId == id);
            if(result.Count == 0) { return new ErrorDataResult<List<Car>>(Messages.ColorNotFound); }
            return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _cardal.Get(c => c.Id == id);
            if(result == null)
            {
                return new ErrorDataResult<Car>(Messages.CarNotFound);
            }
            return new SuccessDataResult<Car>(result, Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_cardal.GetCarDetails(), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            var result = _cardal.GetCarDetailsByBrandId(brandId);
            if (result.Count == 0) { return new ErrorDataResult<List<CarDetailDto>>(result, Messages.BrandNotFound); }
            return new SuccessDataResult<List<CarDetailDto>>(result, Messages.CarsListed);
        }
        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            var result = _cardal.GetCarDetailsByColorId(colorId);
            if(result.Count == 0) { return new ErrorDataResult<List<CarDetailDto>>(Messages.ColorNotFound); }
            return new SuccessDataResult<List<CarDetailDto>>(result, Messages.CarsListed);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            if(_cardal.Get(c=> c.Id == car.Id) == null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }
            _cardal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        private IResult CheckIfColorExists(int colorId)
        {
            var result = _colorService.GetById(colorId);
            if (!result.Success) { return new ErrorResult(result.Message); }
            
            return new SuccessResult();
        }

        private IResult CheckIdBrandExists(int brandId)
        {
            var result = _brandService.GetById(brandId);
            if (!result.Success) { return new SuccessResult(); }
            return new SuccessResult();
        }
    }
}
