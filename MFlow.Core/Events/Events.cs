using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    /// <summary>
    ///     A class to raise and subscribe to events created by the validator
    /// </summary>
    public class Events : IEvents
    {
        private List<Delegate> _actions;
        private List<Delegate> Actions
        {
            get { return _actions ?? (_actions = new List<Delegate>()); }
        }

        /// <summary>
        ///     Register a callback to an event
        /// </summary>
        public IDisposable Register<T>(Action<T> callback)
        {
            Actions.Add(callback);
            return new EventRegistrationRemover(() => Actions.Remove(callback));
        }

        /// <summary>
        ///     Raise an event
        /// </summary>
        public void Raise<T>(T eventArgs)
        {
            foreach (var theAction in Actions.Where(action => action.GetType() == typeof(Action<T>)).Cast<Action<T>>())
            {
                var action = theAction;
                var sameThread = ((IEvent<object>)eventArgs).SameThread;
                if (!sameThread)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(delegate
                        {
                            action(eventArgs);
                        }, Thread.CurrentPrincipal);
                }
                else
                {
                    action(eventArgs);
                }
            }
        }

        private sealed class EventRegistrationRemover : IDisposable
        {
            private readonly Action _callOnDispose;

            public EventRegistrationRemover(Action toCall)
            {
                _callOnDispose = toCall;
            }

            public void Dispose()
            {
                _callOnDispose();
            }
        }
    }
}
