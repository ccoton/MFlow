using MEvents.Core;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MFlow.Core.MessageResolver;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Configuration;
using System;

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
        public IFluentValidationBuilder<T> GetFluentValidation<T>(T target) where T : class
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return GetFluentValidation<T>(target, null, null, null, null, null, null, null);
        }

        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        public IFluentValidationBuilder<T> GetFluentValidation<T>(T target, IPropertyNameResolver propertyNameResolver,
            IResolveValidationMessages messageResolver,  IExpressionBuilder<T> expressionBuilder, IValidatorFactory validatorFactory,
            IBuildConditions<T> validatorToCondition, IEventCoordinator eventCoordinator, IConfigureFluentValidation configuration) where T : class
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (propertyNameResolver == null)
                propertyNameResolver = new PropertyNameResolver();

            if (expressionBuilder == null)
                expressionBuilder = new ExpressionBuilder<T>();

            if (validatorFactory == null)
                validatorFactory = new ValidatorFactory();

            if (configuration == null)
                configuration = Configuration.Configuration.Current;

            if (messageResolver == null)
                messageResolver = Configuration.Configuration.Current.MessageResolverConfiguration.Resolver;

            if (validatorToCondition == null)
                validatorToCondition = new ConditionBuilder<T>(target, expressionBuilder, propertyNameResolver, messageResolver, configuration);

            if (eventCoordinator == null)
                eventCoordinator = new EventsFactory().GetEventCoordinator();

            IFluentValidationBuilder<T> fluentValidation = new FluentValidation<T>(target, propertyNameResolver, messageResolver, expressionBuilder,
                validatorFactory, validatorToCondition, eventCoordinator, configuration);

            if (configuration.StatisticsEnabled)
                fluentValidation = new FluentValidationWithStatistics<T>((IFluentValidation<T>)fluentValidation, configuration.StatisticsConfiguration.Recorder);

            return fluentValidation;
        }
    }
}
