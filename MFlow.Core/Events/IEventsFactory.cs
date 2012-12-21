using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    public interface IEventsFactory
    {
        IEvents GetEventStore();
    }
}
