using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    /// <summary>
    ///     An event that can be raised by the validator
    /// </summary>
    public interface IEvent<out T>
    {
        /// <summary>
        ///     Is the event to be dispatched on the current thread
        /// </summary>
        bool SameThread { get; }

        /// <summary>
        ///     The source of the event
        /// </summary>
        T Source { get; }
    }
}
