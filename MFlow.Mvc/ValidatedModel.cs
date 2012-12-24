using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Validation;

namespace MFlow.Mvc
{
    /// <summary>
    ///     An base class that an MVC view model can inherit from
    /// </summary>
    public class ValidatedModel<T> : IValidatedModel<T>, IValidatableObject
    {
        private IFluentValidation<T> _validator;

        /// <summary>
        ///     Set the target for validation
        /// </summary>
        public void SetTarget(T target)
        {
            _validator = new FluentValidation<T>(target);
        }

        /// <summary>
        ///     The validation instance
        /// </summary>
        public IFluentValidation<T> Validator
        {
            get
            {
                return _validator;
            }
        }

        /// <summary>
        ///     Validtes the current object instance against the validator
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validator.SetTarget((T)validationContext.ObjectInstance);
            var validate = _validator.Validate();
            return validate.Select(v => new ValidationResult(v.Condition.Message, new[] { v.Condition.Key }));
        }

    }
}
