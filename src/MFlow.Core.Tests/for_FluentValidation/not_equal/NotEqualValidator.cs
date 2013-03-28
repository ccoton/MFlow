using MFlow.Core.Internal.Validators.Generic;
using MFlow.Core.Internal.Validators.Strings;
using MFlow.Core.Validation.Validators.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class NotEqualValidator<TInput, TCompare> : INotEqualToValidator<TInput, TCompare>
    {
        public bool Validate(TInput input, TCompare value)
        {
            return input.ToString() == "customnotequalvalidator";
        }
    }
}
