using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Validation.Context
{
	/// <summary>
	///     The context of the current validation
	/// </summary>
    class CurrentValidationContext<T> : ICurrentValidationContext<T>
    {
        readonly object _expression;

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
