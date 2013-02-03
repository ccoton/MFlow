using System;
using MFlow.Core.Validation.Builder;
using MFlow.Core.XmlConfiguration;
using MFlow.Core.Internal;
using MFlow.Core.VmlConfiguration;

namespace MFlow.Core.Validation.Factories
{
    /// <summary>
    ///     A factory to provide a fluentvalidation implementation
    /// </summary>
    public class FluentValidationFactory : IFluentValidationFactory
    {
        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        public IFluentValidationBuilder<T> GetFluentValidation<T>(T target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            var resolver = new PropertyNameResolver();
            var messageResolver = new MessageResolver();
            var expressionBuilder = new ExpressionBuilder<T>();
            var validatorFactory = new ValidatorFactory();
            return new FluentValidation<T>(target, resolver, messageResolver, expressionBuilder, validatorFactory);
        }

        /// <summary>
        ///     Gets a fluent validation implementation loaded from configuration
        /// </summary>
        public IFluentValidationBuilder<T> GetFluentValidationFromConfig<T>(T target, string fileName)
        {

            if (target == null)
                throw new ArgumentNullException("target");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            IFluentValidationLoader loader = null;
            if (fileName.ToLower().EndsWith(".vml", StringComparison.CurrentCultureIgnoreCase))
                loader = new VmlValidationLoader();
            else
                loader = new XmlValidationLoader();

            return (IFluentValidationBuilder<T>)(loader.Load<T>(target, fileName));

        }


    }
}
