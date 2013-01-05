using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Validation.Context
{
    internal interface ICurrentValidationContext<T>
    {
        Expression<Func<T, C>> GetExpression<C>();
        ConditionType ConditionType { get; }
        ConditionOutput ConditionOutput { get; }
    }
}
