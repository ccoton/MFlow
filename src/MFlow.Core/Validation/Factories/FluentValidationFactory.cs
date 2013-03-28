using MEvents.Core;
using MFlow.Core.Configuration;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MFlow.Core.MessageResolver;
using MFlow.Core.Validation.Builder;
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
        public IFluentValidationBuilder<T> CreateFor<T>(T target) where T : class
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return CreateFor<T>(target, null, null, null, null, null, null, null);
        }

        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        public IFluentValidationBuilder<T> CreateFor<T>(T target, IPropertyNameResolver propertyNameResolver,
            IResolveValidationMessages messageResolver, IBuildExpressions expressionBuilder, IValidatorFactory validatorFactory,
            IBuildConditions<T> conditionBuilder, IEventCoordinator eventCoordinator, IConfigureFluentValidation configuration) where T : class
        {
            if (target == null)
                throw new ArgumentNullException("target");

            propertyNameResolver = propertyNameResolver ?? new PropertyNameResolver();
            validatorFactory = validatorFactory ??  new ValidatorFactory();
            configuration = configuration ?? MFlowConfiguration.Current;
            expressionBuilder = expressionBuilder ?? configuration.ExpressionBuilder.Builder;
            messageResolver = messageResolver ?? configuration.MessageResolver.Resolver;
            conditionBuilder = conditionBuilder ?? new ConditionBuilder<T>(target, expressionBuilder, propertyNameResolver, messageResolver, configuration);
            eventCoordinator = eventCoordinator ?? new EventsFactory().GetEventCoordinator();

            IFluentValidationBuilder<T> fluentValidation = new FluentValidation<T>(target, propertyNameResolver, messageResolver, expressionBuilder,
                validatorFactory, conditionBuilder, eventCoordinator);

            if (configuration.Statistics.Enabled)
                fluentValidation = new FluentValidationWithStatistics<T>((IFluentValidation<T>)fluentValidation, 
                    configuration.Statistics.Recorder, expressionBuilder);

            return fluentValidation;
        }
    }
}
