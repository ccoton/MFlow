using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     Represents a fluent condition that applies to a type (T) and has
    ///     a type, key and message
    /// </summary>
    internal class FluentCondition<T> : IFluentCondition<T>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public FluentCondition(Expression<Func<T, bool>> condition, ConditionType type, string key, string message)
        {
            Condition = condition;
            Type = type;
            Key = key;
            Message = message;
        }

        /// <summary>
        ///     The condition
        /// </summary>
        public Expression<Func<T, bool>> Condition { get; private set; }

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
        ///     Set the message
        /// </summary>
        public void SetMessage(string message)
        {
            Message = message;
        }
    }
}
