using System;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MEvents.Core;

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
            return GetFluentValidation<T>(target, null, null, null, null, null, null);
        }

        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        public IFluentValidationBuilder<T> GetFluentValidation<T>(T target, IPropertyNameResolver propertyNameResolver,
            IMessageResolver messageResolver,  IExpressionBuilder<T> expressionBuilder, IValidatorFactory validatorFactory,
            IValidatorToCondition<T> validatorToCondition, IEventCoordinator eventCoordinator) where T : class
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (propertyNameResolver == null)
                propertyNameResolver = new PropertyNameResolver();

            if (messageResolver == null)
                messageResolver = new MessageResolver();

            if (expressionBuilder == null)
                expressionBuilder = new ExpressionBuilder<T>();

            if (validatorFactory == null)
                validatorFactory = new ValidatorFactory();

            if (validatorToCondition == null)
                validatorToCondition = new ValidatorToCondition<T>(target, expressionBuilder, propertyNameResolver, messageResolver);

            if (eventCoordinator == null)
                eventCoordinator = new EventsFactory().GetEventCoordinator();

            return new FluentValidation<T>(target, propertyNameResolver, messageResolver, expressionBuilder,
                validatorFactory, validatorToCondition, eventCoordinator);
        }
    }
}
