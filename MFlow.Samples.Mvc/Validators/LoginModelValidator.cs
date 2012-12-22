using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MFlow.Core.Validation;
using MFlow.Samples.Mvc.Models;

namespace MFlow.Samples.Mvc.Validators
{
    public class LoginModelValidator : IValidatableObject
    {

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var model = (LoginModel)validationContext.ObjectInstance;

            IFluentValidation<LoginModel> validation = new FluentValidation<LoginModel>(model);

            var validate = validation
                .NotNullOrEmpty(m => m.UserName, message: "Username cannot be empty")
                .NotNullOrEmpty(m => m.Password, message: "Password cannot be empty")
                .Validate();

            return validate.Select(v => new ValidationResult(v.Condition.Message, new[] { v.Condition.Key }));

        }

    }
}