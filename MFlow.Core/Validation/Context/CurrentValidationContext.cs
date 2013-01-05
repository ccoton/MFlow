using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Validation.Context
{
    internal class CurrentValidationContext<T> : ICurrentValidationContext<T>
    {
        private readonly object _expression;

        public CurrentValidationContext(object expression, ConditionType conditionType = ConditionType.And, ConditionOutput output = ConditionOutput.Error)
        {
            _expression = expression;
            ConditionType = conditionType;
            ConditionOutput = output;
        }

        public Expression<Func<T, C>> GetExpression<C>()
        {
            return (Expression<Func<T, C>>)_expression;
        }

        public ConditionType ConditionType { get; private set; }
        public ConditionOutput ConditionOutput { get; private set; }
    }
}
