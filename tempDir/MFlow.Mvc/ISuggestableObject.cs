using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MFlow.Mvc
{
    public interface ISuggestableObject
    {
        /// <summary>
        ///      Validates the current object instance against the validator, providing pre validation suggestions
        /// </summary>
        IEnumerable<ValidationResult> Suggest(ValidationContext validationContext);
    }
}

