using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Validation.Checker;

namespace MFlow.Core.Validation.Builder
{
    public interface IFluentValidationBuilder<T>
    {
        /// <summary>
        ///     Sets the target of this validation instance
        /// </summary>
        void SetTarget(T target);

        /// <summary>
        ///     Gets the target of this validation instance
        /// </summary>
        /// <returns></returns>
        T GetTarget();

        /// <summary>
        ///     Sets up a condition
        /// </summary>
        IFluentValidationChecker<T> Check<O>(Expression<Func<T, O>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        IFluentValidation<T> If(bool condition);

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        IFluentValidation<T> And(bool condition);

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        IFluentValidation<T> Or(bool condition);

        /// <summary>
        ///     Returns a boolean indicating if this validator is satisfied
        /// </summary>
        bool Satisfied(bool suppressWarnings = true);

        /// <summary>
        ///     Validate this instance
        /// </summary>
        IEnumerable<IValidationResult<T>> Validate(bool suppressWarnings = true);
    }
}
