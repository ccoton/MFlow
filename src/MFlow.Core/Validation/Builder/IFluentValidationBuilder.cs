using MFlow.Core.Conditions.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.Validation.Builder
{
    public interface IFluentValidationBuilder<T>
    {
        /// <summary>
        ///     Sets the target of this validation instance
        /// </summary>
        IFluentValidationBuilder<T> SetTarget(T target);

        /// <summary>
        ///     Gets the target of this validation instance
        /// </summary>
        /// <returns></returns>
        T GetTarget();

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationGeneric<T> Check<O>(Expression<Func<T, O>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationString<T> Check(Expression<Func<T, string>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationNumber<T> Check(Expression<Func<T, int>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationNumber<T> Check(Expression<Func<T, int?>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationDate<T> Check(Expression<Func<T, DateTime>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationDate<T> Check(Expression<Func<T, DateTime?>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationCollection<T> Check<O>(Expression<Func<T, ICollection<O>>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Group a set of validation checks into a set
        /// </summary>
        IFluentValidation<T> Group(string name);

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
        bool Satisfied(string group = "", bool suppressWarnings = true);

        /// <summary>
        ///     Validate this instance
        /// </summary>
        IEnumerable<IValidationResult<T>> Validate(string group = "", bool suppressWarnings = true);
    }
}
