using MEvents.Core;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Configuration;

namespace MFlow.Core.Validation.Factories
{
    /// <summary>
    ///     A factory to provide an fluentvalidation implementation
    /// </summary>
    public interface IFluentValidationFactory
    {
        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        IFluentValidationBuilder<T> GetFluentValidation<T>(T target) where T : class;

        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        IFluentValidationBuilder<T> GetFluentValidation<T>(T target, IPropertyNameResolver propertyNameResolver,
           IMessageResolver messageResolver, IExpressionBuilder<T> expressionBuilder, IValidatorFactory validatorFactory,
           IConvertValidatorToCondition<T> validatorToCondition, IEventCoordinator eventCoordinator, IConfigureFluentValidation configuration) where T : class;
    }
}
