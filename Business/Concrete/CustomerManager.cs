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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        [ValidationAspect(typeof(CustomerValidator))]
        [SecuredOperation("admin")]
        public IResult Add(Customer customer)
        {
            if (_customerDal.Get(c => c.UserId == customer.UserId) != null)
            {
                return new ErrorResult(Messages.CustomerAlreadyExists);
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Customer customer)
        {
            if (_customerDal.Get(c => c.UserId == customer.UserId) == null)
            {
                return new ErrorResult(Messages.CustomerNotFound);
            }
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            if (_customerDal.Get(c => c.UserId == customerId) == null)
            {
                return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
            }
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == customerId));
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            if (_customerDal.Get(c => c.UserId == customer.UserId) == null)
            {
                return new ErrorResult(Messages.CustomerNotFound);
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
