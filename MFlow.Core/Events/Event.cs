using System;

namespace MFlow.Core.Events
{
    /// <summary>
    ///     An event that can be raised by the validator
    /// </summary>
    public class Event<T> : IEvent<T>
    {
        readonly T _source;
        readonly bool _sameThread;

        /// <summary>
        ///     Constructor
        /// </summary>
        protected Event(T source, bool sameThread)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            _source = source;
            _sameThread = sameThread;
        }

        /// <summary>
        ///     Is the event to be dispatched on the current thread
        /// </summary>
        public bool SameThread
        {
            get { return _sameThread; }
        }

        /// <summary>
        ///     The source of the event
        /// </summary>
        public T Source
        {
            get { return _source; }
        }
    }
}
