namespace MFlow.Core.Events
{
    /// <summary>
    ///     A method of exposing an instance of the IEvents interface
    /// </summary>
    public class EventsFactory : IEventsFactory
    {
        static IEvents _events;

        /// <summary>
        ///     Static constructor
        /// </summary>
        static EventsFactory()
        {
            _events = new Events();
        }

        /// <summary>
        ///    Gets the event implementations
        /// </summary>
        public IEvents GetEventStore()
        {
            return _events;
        }
    }
}
