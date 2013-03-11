using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MFlow.Core.Validation.Builder;
using System;
using MFlow.Core.Validation;

namespace MFlow.Mvc
{
    /// <summary>
    ///     An interface representing an MVC view model that can be validated
    /// </summary>
    public interface IValidatedModel<T> where T : class
    {
        /// <summary>
        ///     Sets the target for validation
        /// </summary>
        IFluentValidationBuilder<T> GetValidator(T target, string rulesetFile = "", Func<IFluentValidationLoader> loader = null);

        /// <summary>
        ///     Validates the current object instance against the validator
        /// </summary>
        IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        /// <summary>
        ///      Validates the current object instance against the validator, providing pre validation suggestions
        /// </summary>
        IEnumerable<ValidationResult> Suggest(ValidationContext validationContext);
    }
}
