using System;
using Entities.Concrete;
using FluentValidation;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator:AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {

            RuleFor(c => c.Id).Empty();

            RuleFor(c => c.ImagePath).Empty();

            RuleFor(c => c.Date).Empty();
        }
    }
}
