﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Events;

namespace MFlow.Core.Validation
{
    public partial interface IFluentValidation<T>
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
        ///     Adds an expression to the chain 
        /// </summary>
        IFluentValidationChecker<T> Check<O>(Expression<Func<T, O>> expression, ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     When applied to a Check make it behave as a warning, by default will not be raised when validation occurs
        /// </summary>
        IFluentValidation<T> Warn();

        /// <summary>
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        IFluentValidation<T> If(bool condition);

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean IF condition, then evaluates it
        /// </summary>
        IFluentValidation<T> If(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        IFluentValidation<T> And(bool condition);

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        IFluentValidation<T> And(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        IFluentValidation<T> Or(bool condition);

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        IFluentValidation<T> Or(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     Add a key to a validation expression
        /// </summary>
        IFluentValidation<T> Key(string key);

        /// <summary>
        ///     Add a message to a validation expression
        /// </summary>
        IFluentValidation<T> Message(string message);

        /// <summary>
        ///     Add a hint to a validation expression
        /// </summary>
        IFluentValidation<T> Hint(string hint);

        /// <summary>
        ///     Takes an action to execute if the validator is satisfied
        /// </summary>
        IFluentValidation<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current);

        /// <summary>
        ///     Takes an action to execute if the validator is not satisfied
        /// </summary>
        IFluentValidation<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current);

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator);

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        IFluentValidation<T> DependsOn<D>(Expression<Func<T, D>> validator) where D : IFluentValidation<T>;
        
        /// <summary>
        ///     Raises an event
        /// </summary>
        IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>;

        /// <summary>
        ///     Clears the validation conditions
        /// </summary>
        /// <returns></returns>
        IFluentValidationBuilder<T> Clear();

        /// <summary>
        ///     Validate this instance
        /// </summary>
        IEnumerable<IValidationResult<T>> Validate(bool suppressWarnings = true);

        /// <summary>
        ///     Validate this instance
        /// </summary>
        IEnumerable<IValidationResult<T>> ValidateAndThrow<E>(bool suppressWarnings = true) where E : Exception, new();

        /// <summary>
        ///     Throws an exception
        /// </summary>
        void Throw<E>(E exception) where E : Exception;

        /// <summary>
        ///     Returns a boolean indicating if this validator is satisfied
        /// </summary>
        bool Satisfied(bool suppressWarnings = true);
    }
}
