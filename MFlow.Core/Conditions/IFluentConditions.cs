using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    public interface IFluentConditions<T>
    {
        IFluentConditions<T> If(bool condition, string key = "", string message = "");
        IFluentConditions<T> And(bool condition, string key = "", string message = "");
        IFluentConditions<T> Or(bool condition, string key = "", string message = "");
        IFluentConditions<T> Clear();
        IFluentConditions<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current);
        IFluentConditions<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current);
        IEnumerable<IFluentCondition<T>> Conditions { get; }
        bool Satisfied();
    }

    public interface IFluentCondition<T>
    {
        Expression<Func<T, bool>> Condition { get; }
        ConditionType Type { get; }
        string Key { get; }
        string Message { get; }
    }

    public enum ConditionType
    {
        And,
        Or
    }

    public enum ExecuteThread
    {
        Current,
        New
    }
}
