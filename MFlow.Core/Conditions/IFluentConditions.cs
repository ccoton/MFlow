using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    public interface IFluentConditions
    {
        IFluentConditions If(bool condition);
        IFluentConditions And(bool condition);
        IFluentConditions Or(bool condition);
        IFluentConditions Clear();
        IFluentConditions Then(Action execute, ExecuteOptions options = ExecuteOptions.SameThread);
        IFluentConditions Else(Action execute, ExecuteOptions options = ExecuteOptions.SameThread);
        bool Satisfied();
    }

    internal interface IFluentCondition
    {
        bool Condition { get;  }
        ConditionType Type { get;  }
    }

    internal enum ConditionType
    {
        And,
        Or
    }

    public enum ExecuteOptions
    {
        SameThread,
        NewThread
    }
}
