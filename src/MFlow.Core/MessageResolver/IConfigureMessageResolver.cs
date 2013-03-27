
namespace MFlow.Core.MessageResolver
{
    /// <summary>
    ///     A contract for configuring a message resolver
    /// </summary>
    public interface IConfigureMessageResolver
    {
        /// <summary>
        ///     The message resolver
        /// </summary>
        IResolveValidationMessages Resolver { get; }
    }
}
