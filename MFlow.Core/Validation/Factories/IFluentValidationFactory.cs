﻿using MFlow.Core.Validation.Builder;

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
        ///     Gets a fluent validation implementation loaded from configuration
        /// </summary>
        IFluentValidationBuilder<T> GetFluentValidationFromConfig<T>(T target, string fileName) where T : class;
    }
}
