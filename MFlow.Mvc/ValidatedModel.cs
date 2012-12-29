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
        private IFluentValidationFactory _factory;

        /// <summary>
        ///     Set the target for validation
        /// </summary>
        public void SetTarget(T target, bool loadRuleset = false, string rulesetFile = "")
        {
            _factory = new FluentValidationFactory();
            _validator = _factory.GetFluentValidation<T>(target, loadRuleset, rulesetFile);
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
