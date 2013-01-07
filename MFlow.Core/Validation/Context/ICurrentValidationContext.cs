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
    internal interface ICurrentValidationContext<T>
    {
		/// <summary>
		///     Gets the current expression
		/// </summary>
        Expression<Func<T, C>> GetExpression<C>();

		/// <summary>
		///     Gets the current condition type
		/// </summary>
        ConditionType ConditionType { get; }

		/// <summary>
		///     Gets the current condition output
		/// </summary>
        ConditionOutput ConditionOutput { get; }
    }
}
