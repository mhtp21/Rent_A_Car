using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Concrete;
using Core.Utilities.FileOperations;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinesRules.Run(CheckIfCarExists(carImage.CarId), CheckIfCarImageLimitExceeded(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(carImage.ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var findCarImage = _carImageDal.Get(c => c.Id == carImage.Id);
            if (findCarImage == null)
            {
                return new ErrorResult(Messages.CarImageNotFound);
            }

            var result = FileHelper.Delete(carImage.ImagePath);
            if (!result.Success)
            {
                return result;
            }

            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        public IDataResult<CarImage> GetById(int id)
        {
            var carImage = _carImageDal.Get(c => c.Id == id);
            if (carImage == null)
            {
                return new ErrorDataResult<CarImage>(Messages.CarImageNotFound);
            }

            return new SuccessDataResult<CarImage>(carImage);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var result = CheckIfCarExists(carId);
            if (!result.Success)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            var data = _carImageDal.GetAll(c => c.CarId == carId).Count == 0
                ? new List<CarImage> { new CarImage { ImagePath = default } }
                : _carImageDal.GetAll(c => c.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(data);
        }

        //Business-Rules
        private IResult CheckIfCarExists(int carId)
        {
            IDataResult<Car> result = _carService.GetById(carId);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}