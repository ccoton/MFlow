using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Validation;

namespace MFlow.Mvc
{
    public interface IValidatedModel<T>
    {
        void SetTarget(T target);
        IFluentValidation<T> Validator { get; }
        IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
