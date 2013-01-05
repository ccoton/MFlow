using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     A fluent conditions interface
    /// </summary>
    public interface IFluentConditions<T>
    {
        /// <summary>
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        IFluentConditions<T> If(bool condition, string key = "", string message = "");

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean IF condition, then evaluates it
        /// </summary>
        IFluentConditions<T> If(Expression<Func<T, bool>> expression, string key="", string message = "");

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        IFluentConditions<T> And(bool condition, string key = "", string message = "");

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        IFluentConditions<T> And(Expression<Func<T, bool>> expression, string key = "", string message = "");

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        IFluentConditions<T> Or(bool condition, string key = "", string message = "");

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        IFluentConditions<T> Or(Expression<Func<T, bool>> expression, string key = "", string message = "");

        /// <summary>
        ///     Clear the conditions for the validation instance
        /// </summary>
        IFluentConditions<T> Clear();

        /// <summary>
        ///     Takes an action to execute if the validator is satisfied
        /// </summary>
        IFluentConditions<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current);

        /// <summary>
        ///     Takes an action to execute if the validator is not satisfied
        /// </summary>
        IFluentConditions<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current);

        /// <summary>
        ///     Returns a boolean indicating if this validator is satisfied
        /// </summary>
        bool Satisfied();
    }
}
