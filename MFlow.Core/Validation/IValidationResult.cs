using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;

namespace MFlow.Core.Validation
{
    public interface IValidationResult<T>
    {
        IFluentCondition Condition { get; }
    }
}
