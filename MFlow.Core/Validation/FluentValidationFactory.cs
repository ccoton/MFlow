﻿using MFlow.Core.XmlConfiguration;
using MFlow.Core.Internal;
using MFlow.Core.VmlConfiguration;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A factory to provide an fluentvalidation implementation
    /// </summary>
    public class FluentValidationFactory : IFluentValidationFactory
    {
        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        public IFluentValidationBuilder<T> GetFluentValidation<T>(T target, bool loadXmlRuleset = false, 
                                                                  string fileName = "")
        {

            var resolver = new PropertyNameResolver();
            var messageResolver = new MessageResolver();
            var expressionBuilder = new ExpressionBuilder<T>();
            var validatorFactory = new ValidatorFactory();

            if (!loadXmlRuleset)
                return new FluentValidation<T>(target, resolver, messageResolver, expressionBuilder, validatorFactory);
            IFluentValidationLoader loader = null; 
            if (fileName.ToLower().EndsWith(".vml"))
                loader = new VmlValidationLoader();
            else 
                loader = new XmlValidationLoader();

            return (IFluentValidationBuilder<T>)(string.IsNullOrEmpty(fileName) ? loader.Load<T>(target) : loader.Load<T>(target, fileName));
        }
    }
}
