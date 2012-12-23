using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Validation;

namespace MFlow.Mvc
{
    public class ValidatedModel<T> : IValidatedModel<T>, IValidatableObject
    {
        private IFluentValidation<T> _validator;

        public void SetTarget(T target)
        {
            _validator = new FluentValidation<T>(target);
        }

        public IFluentValidation<T> Validator
        {
            get
            {
                return _validator;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validator.SetTarget((T)validationContext.ObjectInstance);
            var validate = _validator.Validate();
            return validate.Select(v => new ValidationResult(v.Condition.Message, new[] { v.Condition.Key }));
        }

    }
}
