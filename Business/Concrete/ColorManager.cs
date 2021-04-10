using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            IResult result = BusinesRules.Run(CheckIfColorNameExists(color.Name));
            if (!result.Success)
            {
                return result;
            }

            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Color color)
        {
            if (_colorDal.Get(c => c.Id == color.Id) == null)
            {
                return new ErrorResult(Messages.ColorNotFound);
            }
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<Color> GetById(int id)
        {
            if (_colorDal.Get(c => c.Id == id) == null)
            {
                return new ErrorDataResult<Color>(Messages.ColorNotFound);
            }
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == id), Messages.ColorsListed);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            if (_colorDal.Get(c => c.Id == color.Id) == null)
            {
                return new ErrorResult(Messages.ColorNotFound);
            }
            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        private IResult CheckIfColorNameExists(string colorName)
        {
            if (_colorDal.Get(c => c.Name.ToLower() == colorName.ToLower()) != null)
            {
                return new ErrorResult(Messages.ColorAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}