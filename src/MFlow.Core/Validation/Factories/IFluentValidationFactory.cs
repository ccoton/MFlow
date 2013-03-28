using MEvents.Core;
using MFlow.Core.Configuration;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MFlow.Core.MessageResolver;
using MFlow.Core.Validation.Builder;

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
        IFluentValidationBuilder<T> CreateFor<T>(T target) where T : class;

        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        IFluentValidationBuilder<T> CreateFor<T>(T target, IPropertyNameResolver propertyNameResolver,
           IResolveValidationMessages messageResolver, IBuildExpressions expressionBuilder, IValidatorFactory validatorFactory,
           IBuildConditions<T> validatorToCondition, IEventCoordinator eventCoordinator, IConfigureFluentValidation configuration) where T : class;
    }
}
