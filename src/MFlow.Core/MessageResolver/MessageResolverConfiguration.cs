using System;

namespace MFlow.Core.MessageResolver
{
    /// <summary>
    ///     An implementation of message resolver configuration 
    /// </summary>
    public class MessageResolverConfiguration : IConfigureMessageResolver
    {
        readonly IResolveValidationMessages _resolver;

        /// <summary>
        ///     Constructor
        /// </summary>
        public MessageResolverConfiguration(IResolveValidationMessages resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException("resolver");

            _resolver = resolver;
        }

        /// <summary>
        ///     The resolver
        /// </summary>
        public IResolveValidationMessages Resolver
        {
            get { return _resolver; }
        }
    }
}
