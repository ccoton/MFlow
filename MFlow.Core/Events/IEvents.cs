using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
