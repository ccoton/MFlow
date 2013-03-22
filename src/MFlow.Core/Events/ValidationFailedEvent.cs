using MEvents.Core;
using MFlow.Core.Validation;

namespace MFlow.Core.Events
{
    /// <summary>
    ///     An event that can be raised when validation fails
    /// </summary>
    public class ValidationFailedEvent<T> : Event<IFluentValidation<T>>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ValidationFailedEvent(IFluentValidation<T> validator)
            : base(validator)
        { }
    }
}
