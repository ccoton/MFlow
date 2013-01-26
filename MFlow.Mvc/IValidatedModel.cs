using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MFlow.Core.Validation.Builder;

namespace MFlow.Mvc
{
    /// <summary>
    ///     An interface representing an MVC view model that can be validated
    /// </summary>
    public interface IValidatedModel<T>
    {
        /// <summary>
        ///     Sets the target for validation
        /// </summary>
        IFluentValidationBuilder<T> GetValidator(T target, bool loadRuleset = false, string rulesetFile = "");

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
