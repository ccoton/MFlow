using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Internal.Validators
{
    internal interface IValidator<TInput>
    {
        bool Validate(TInput input);
    }
}
