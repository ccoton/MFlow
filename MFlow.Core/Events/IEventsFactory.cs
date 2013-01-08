
namespace MFlow.Core.Events
{
    /// <summary>
    ///     A method of exposing an instance of the IEvents interface
    /// </summary>
    public interface IEventsFactory
    {
        /// <summary>
        ///    Gets the event implementations
        /// </summary>
        IEvents GetEventStore();
    }
}
