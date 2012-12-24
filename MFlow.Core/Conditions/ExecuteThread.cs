using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Conditions
{
    /// <summary>
    ///     An enum represeting a choice of thread options to use 
    ///     when calling a fluent method that invokes something
    /// </summary>
    public enum ExecuteThread
    {
        Current,
        New
    }
}
