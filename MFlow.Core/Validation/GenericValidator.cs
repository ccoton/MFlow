using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Validation
{
    public class GenericValidator : FluentValidation<object>
    {
        public GenericValidator()
            : base(new object())
        {
        }
    }
}
