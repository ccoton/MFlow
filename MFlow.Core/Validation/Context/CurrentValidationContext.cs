using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Validation.Context
{
	/// <summary>
	///     The context of the current validation
	/// </summary>
    internal class CurrentValidationContext<T> : ICurrentValidationContext<T>
    {
        private readonly object _expression;

        public CurrentValidationContext(object expression, ConditionType conditionType = ConditionType.And, ConditionOutput output = ConditionOutput.Error)
        {
            _expression = expression;
            ConditionType = conditionType;
            ConditionOutput = output;
        }

		/// <summary>
		///     Gets the current expression
		/// </summary>
        public Expression<Func<T, C>> GetExpression<C>()
        {
            return (Expression<Func<T, C>>)_expression;
        }

		/// <summary>
		///     Gets the current condition type
		/// </summary>
        public ConditionType ConditionType { get; private set; }

		/// <summary>
		///     Gets the current condition output
		/// </summary>
        public ConditionOutput ConditionOutput { get; private set; }
    }
}
