using System;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinesRules.Run(CheckIfBrandNameExists(brand.Name));
            if (result != null)
            {
                return result;
            }

            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult Delete(Brand brand)
        {
            IResult result = CheckIfBrandExists(brand.BrandId);
            if (!result.Success)
            {
                return result;
            }

            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IDataResult<Brand> GetById(int id)
        {
            IResult result = CheckIfBrandExists(id);
            if (!result.Success)
            {
                return new ErrorDataResult<Brand>(result.Message);
            }
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == id));
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            IResult result = CheckIfBrandExists(brand.BrandId);
            if (!result.Success)
            {
                return result;
            }

            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        private IResult CheckIfBrandNameExists(string brandName)
        {
            if (_brandDal.Get(b => b.Name.ToLower() == brandName.ToLower()) != null)
            {
                return new ErrorResult(Messages.BrandAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandExists(int brandId)
        {
            if (_brandDal.Get(b => b.BrandId == brandId) == null)
            {
                return new ErrorResult(Messages.BrandNotFound);
            }
            return new SuccessResult();
        }
    }
}