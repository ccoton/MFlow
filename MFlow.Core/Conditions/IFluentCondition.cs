using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     Represents a fluent condition that applies to a type (T) and has
    ///     a type, key and message
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFluentCondition<T>
    {
        /// <summary>
        ///     The condition
        /// </summary>
        Expression<Func<T, bool>> Condition { get; }

        /// <summary>
        ///     The type of the condition
        /// </summary>
        ConditionType Type { get; }

        /// <summary>
        ///     The output of the condition
        /// </summary>
        ConditionOutput Output { get; }

        /// <summary>
        ///     A key to identify to condition
        /// </summary>
        string Key { get; }

        /// <summary>
        ///     A message to display when the condition isn't satisfied
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     A message to display as a help indicator
        /// </summary>
        string Hint { get; }

        /// <summary>
        ///     Sets the key.
        /// </summary>
        void SetKey(string key);

        /// <summary>
        ///     Sets the message.
        /// </summary>
        void SetMessage(string message);

        /// <summary>
        ///     Sets the hint.
        /// </summary>
        void SetHint(string hint);
    }
}
