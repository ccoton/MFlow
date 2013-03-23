using MEvents.Core;
using MFlow.Core.Validation;

namespace MFlow.Core.Events
{
    /// <summary>
    ///     An event that can be raised when a successful validation occurs
    /// </summary>
    public class ValidatedEvent<T> : Event<IFluentValidation<T>>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ValidatedEvent(IFluentValidation<T> validator)
            : base(validator)
        { }
    }
}
