using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation interface
    /// </summary>
    public partial interface IFluentValidation<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression 
        /// </summary>
        IFluentValidation<T> IsEqual<C>(Expression<Func<T, C>> valueExpression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value 
        /// </summary>
        IFluentValidation<T> IsEqual<C>(C value, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value expression 
        /// </summary>
        IFluentValidation<T> IsNotEqual<C>(Expression<Func<T, C>> valueExpression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value 
        /// </summary>
        IFluentValidation<T> IsNotEqual<C>(C value, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator);

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        IFluentValidation<T> DependsOn<D>(Expression<Func<T, D>> validator) where D : IFluentValidation<T>;
    }
}
