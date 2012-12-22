using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    public interface IFluentConditions
    {
        IFluentConditions If(bool condition, string key = "", string message = "");
        IFluentConditions And(bool condition, string key = "", string message = "");
        IFluentConditions Or(bool condition, string key = "", string message = "");
        IFluentConditions Clear();
        IFluentConditions Then(Action execute, ExecuteThread options = ExecuteThread.Current);
        IFluentConditions Else(Action execute, ExecuteThread options = ExecuteThread.Current);
        IEnumerable<IFluentCondition> Conditions { get; }
        bool Satisfied();
    }

    public interface IFluentCondition
    {
        bool Condition { get; }
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
