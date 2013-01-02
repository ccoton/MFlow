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
    /// <typeparam name="T"></typeparam>
    public interface IFluentCondition<T>
    {
        Expression<Func<T, bool>> Condition { get; }
        ConditionType Type { get; }
        string Key { get; }
        string Message { get; }

        void SetKey(string key);
        void SetMessage(string message);
    }
}
