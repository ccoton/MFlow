using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    public interface IEvent<out T>
    {
        bool SameThread { get; }
        T Source { get; }
    }
}
