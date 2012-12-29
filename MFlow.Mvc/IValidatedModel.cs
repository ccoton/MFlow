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
    ///     An interface representing an MVC view model that can be validated
    /// </summary>
    public interface IValidatedModel<T>
    {
        /// <summary>
        ///     Sets the target for validation
        /// </summary>
        void SetTarget(T target, bool loadRuleset = false, string rulesetFile = "");

        /// <summary>
        ///     The validation instance
        /// </summary>
        IFluentValidation<T> Validator { get; }

        /// <summary>
        ///     Validtes the current object instance against the validator
        /// </summary>
        IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
