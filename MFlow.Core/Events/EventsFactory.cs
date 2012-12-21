using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    public class EventsFactory : IEventsFactory
    {
        private static IEvents _events;

        static EventsFactory()
        {
            _events = new Events();
        }

        public IEvents GetEventStore()
        {
            return _events;
        }
    }
}
