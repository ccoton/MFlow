using MFlow.Core.Internal.Validators.Generic;
using MFlow.Core.Internal.Validators.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class EqualValidator<TInput, TCompare> : IEqualToValidator<TInput, TCompare>
    {
        public bool Validate(TInput input, TCompare value)
        {
            return input.ToString() == "customequalvalidator";
        }
    }
}
