using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
    
namespace MFlow.Mvc
{
    /// <summary>
    ///     An base class that an MVC view model can inherit from
    /// </summary>
    public class ValidatedModel<T> : IValidatedModel<T>, IValidatableObject, ISuggestableObject
    {
        IFluentValidationBuilder<T> _validator;
        IFluentValidationFactory _factory;

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
        public IFluentValidationBuilder<T> Validator
        {
            get
            {
                return _validator;
            }
        }
       
        /// <summary>
        ///     Validates the current object instance against the validator
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validator.SetTarget((T)validationContext.ObjectInstance);
            var validate = _validator.Validate().ToList();
            return validate.Select(v => new ValidationResult(v.Condition.Message, new[] { v.Condition.Key })); 
        }

        /// <summary>
        ///      Validates the current object instance against the validator, providing pre validation suggestions
        /// </summary>
        public IEnumerable<ValidationResult> Suggest(ValidationContext validationContext)
        {            
            _validator.SetTarget((T)validationContext.ObjectInstance);
            var validate = _validator.Validate().ToList();
            return validate.Where(v => !string.IsNullOrEmpty(v.Condition.Hint)).Select(v => new ValidationResult(v.Condition.Hint, new[] { v.Condition.Key })); 

        }

    }
}
