using System;

namespace MFlow.Core.Events
{
    /// <summary>
    ///     A interface to raise and subscribe to events created by the validator
    /// </summary>
    public interface IEvents
    {
        /// <summary>
        ///     Raise an event
        /// </summary>
        void Raise<T>(T eventArgs);

        /// <summary>
        ///     Register a callback to an event
        /// </summary>
        IDisposable Register<T>(Action<T> callback);
    }
}
