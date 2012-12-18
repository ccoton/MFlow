using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core
{
    public interface IFluentConditions
    {
        IFluentConditions And(bool condition);
        IFluentConditions Or(bool condition);
        bool Is(bool condition);
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
}
