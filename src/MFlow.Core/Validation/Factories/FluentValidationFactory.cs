﻿using System;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;

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

            var resolver = new PropertyNameResolver();
            var messageResolver = new MessageResolver();
            var expressionBuilder = new ExpressionBuilder<T>();
            var validatorFactory = new ValidatorFactory();
            var validatorToCondition = new ValidatorToCondition<T>(target, expressionBuilder, resolver, messageResolver);
            return new FluentValidation<T>(target, resolver, messageResolver, expressionBuilder, validatorFactory, validatorToCondition);
        }
    }
}