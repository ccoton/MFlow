using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;

namespace MFlow.Core.Validation
{
    public class ValidationResult<T> : IValidationResult<T>
    {

        public ValidationResult(IFluentCondition<T> condition)
        {
            Condition = condition;
        }

        public IFluentCondition<T> Condition { get; private set; }
    }
}
