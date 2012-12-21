using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    public class Event<T> : IEvent<T>
    {
        private readonly T _source;
        private readonly bool _sameThread;

        protected Event(T source, bool sameThread)
        {
            _source = source;
            _sameThread = sameThread;
        }

        public bool SameThread
        {
            get { return _sameThread; }
        }

        public T Source
        {
            get { return _source; }
        }
    }
}
