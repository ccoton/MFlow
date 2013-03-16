using MEvents.Core;
using MFlow.Core.Validation;

namespace MFlow.Core.Events
{
    public class ValidatedEvent<T> : Event<IFluentValidation<T>>
    {
        public ValidatedEvent(IFluentValidation<T> validator)
            : base(validator)
        { }
    }
}
