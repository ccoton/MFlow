using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     Represents a fluent condition that applies to a type (T) and has
    ///     a type, key and message
    /// </summary>
    class FluentCondition<T> : IFluentCondition<T>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public FluentCondition(Expression<Func<T, bool>> condition, ConditionType type, string key, string message, string hint, ConditionOutput output = ConditionOutput.Error)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            Condition = condition;
            Type = type;
            Key = key;
            Message = message;
            Hint = hint;
            Output = output;
        }

        /// <summary>
        ///     The condition
        /// </summary>
        public Expression<Func<T, bool>> Condition { get; private set; }

        /// <summary>
        ///     The output of the condition, an error or warning etc
        /// </summary>
        public ConditionOutput Output { get; private set; }

        /// <summary>
        ///     The type of the condition
        /// </summary>
        public ConditionType Type { get; private set; }

        /// <summary>
        ///     The key used to identify the condition
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        ///     The message used when the condition is not met
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        ///     A message to display as a help indicator
        /// </summary>
        public string Hint { get; private set; }

        /// <summary>
        ///     Set the message
        /// </summary>
        public void SetMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Sets the hint.
        /// </summary>
        public void SetHint(string hint)
        {
            Hint = hint;
        }

        /// <summary>
        ///     Set the key
        /// </summary>
        public void SetKey(string key)
        {
            Key = key;
        }
    }
}
