using System;
using Core.Utilities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
        IDataResult<Customer> GetById(int customerId);
        IDataResult<List<Customer>> GetAll();
    }
}
