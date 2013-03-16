using MEvents.Core;
using MFlow.Core.Validation;

namespace MFlow.Core.Events
{
    public class ValidationFailedEvent<T> : Event<IFluentValidation<T>>
    {
        public ValidationFailedEvent(IFluentValidation<T> validator)
            : base(validator)
        { }
    }
}
