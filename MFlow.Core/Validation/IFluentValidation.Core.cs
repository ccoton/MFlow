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
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        IFluentValidation<T> If(bool condition, string key = "", string message = "");

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean IF condition, then evaluates it
        /// </summary>
        IFluentValidation<T> If(Expression<Func<T, bool>> expression, string message = "");

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        IFluentValidation<T> And(bool condition, string key = "", string message = "");

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        IFluentValidation<T> And(Expression<Func<T, bool>> expression, string message = "");

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        IFluentValidation<T> Or(bool condition, string key = "", string message = "");

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        IFluentValidation<T> Or(Expression<Func<T, bool>> expression, string message = "");

        /// <summary>
        ///     Takes an action to execute if the validator is satisfied
        /// </summary>
        IFluentValidation<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current);

        /// <summary>
        ///     Takes an action to execute if the validator is not satisfied
        /// </summary>
        IFluentValidation<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current);

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean condition, then evaluates it
        /// </summary>
        IFluentValidation<T> Custom(Func<T, bool> function, string key="", string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Raises an event
        /// </summary>
        IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>;

        /// <summary>
        ///     Clears the validation conditions
        /// </summary>
        /// <returns></returns>
        IFluentValidation<T> Clear();

        /// <summary>
        ///     Validate this instance
        /// </summary>
        IEnumerable<IValidationResult<T>> Validate();

        /// <summary>
        ///     Throws an exception
        /// </summary>
        void Throw<E>(E exception) where E : Exception;

        /// <summary>
        ///     Returns a boolean indicating if this validator is satisfied
        /// </summary>
        bool Satisfied();
    }
}
