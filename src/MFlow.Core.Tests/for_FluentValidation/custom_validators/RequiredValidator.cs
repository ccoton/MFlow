using MFlow.Core.Internal.Validators.Generic;
using MFlow.Core.Internal.Validators.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.custom_validators
{
    public class RequiredValidator<T> : IRequiredValidator<T>
    {
        public bool Validate(T input)
        {
            return input.ToString() == "customrequiredvalidator";
        }
    }
}
