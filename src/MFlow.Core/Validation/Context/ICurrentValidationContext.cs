using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Conditions;

namespace MFlow.Core.Validation.Context
{
    /// <summary>
    ///     The context of the current validation
    /// </summary>
    public interface ICurrentValidationContext<T>
    {
        /// <summary>
        ///     Gets the current expression
        /// </summary>
        Expression<Func<T, C>> GetExpression<C> ();

        /// <summary>
        ///     Gets the current nullable expression
        /// </summary>
        Expression<Func<T, Nullable<C>>> GetNullableExpression<C>() where C : struct;

        /// <summary>
        ///     Gets the current condition type
        /// </summary>
        ConditionType ConditionType { get; }

        /// <summary>
        ///     Gets the current condition output
        /// </summary>
        ConditionOutput ConditionOutput { get; }

        /// <summary>
        ///     Does the last expression evaluate to a nullable
        /// </summary>
        bool IsNullable { get; }
    }
}
