using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Internal.Validators.Extension
{
    /// <summary>
    ///     Extension methods for a colection of validators
    /// </summary>
    public static class ValidatorCollectionExtension
    {
        /// <summary>
        ///     Gets the validators that should be applied from a collection
        /// </summary>
        public static IEnumerable<IValidator<T>> ToApply<T>(this ICollection<IValidator<T>> validators, IConfigureFluentValidation configuration)
        {
            foreach(var validator in validators)
            {
                var noExternals = validators.Count == 1;
                var internalValidator = validator.GetType().Assembly == typeof(ValidatorCollectionExtension).Assembly;
                var applyInternalValidator = (noExternals) || (internalValidator && configuration.CustomImplementationMode != CustomImplementationMode.Replace);
                var applyExternalValidator = !internalValidator && (configuration.CustomImplementationMode == CustomImplementationMode.Combine || configuration.CustomImplementationMode == CustomImplementationMode.Replace);

                if (applyInternalValidator || applyExternalValidator)
                {
                    yield return validator;
                }
            }
        }

        /// <summary>
        ///     Gets the comparison validators that should be applied from a collection
        /// </summary>
        public static IEnumerable<IComparisonValidator<TInput, TCompare>> ToApply<TInput, TCompare>(this ICollection<IComparisonValidator<TInput, TCompare>> validators, IConfigureFluentValidation configuration)
        {
            foreach (var validator in validators)
            {
                var noExternals = validators.Count == 1;
                var internalValidator = validator.GetType().Assembly == typeof(ValidatorCollectionExtension).Assembly;
                var applyInternalValidator = (noExternals) || (internalValidator && configuration.CustomImplementationMode != CustomImplementationMode.Replace);
                var applyExternalValidator = !internalValidator && (configuration.CustomImplementationMode == CustomImplementationMode.Combine || configuration.CustomImplementationMode == CustomImplementationMode.Replace);

                if (applyInternalValidator || applyExternalValidator)
                {
                    yield return validator;
                }
            }
        }

        /// <summary>
        ///     Gets the comparison validators that should be applied from a collection
        /// </summary>
        public static IEnumerable<IComparisonValidator<ICollection<TInput>, ICollection<TCompare>>> ToApply<TInput, TCompare>(this ICollection<IComparisonValidator<ICollection<TInput>, ICollection<TCompare>>> validators, IConfigureFluentValidation configuration)
        {
            foreach (var validator in validators)
            {
                var noExternals = validators.Count == 1;
                var internalValidator = validator.GetType().Assembly == typeof(ValidatorCollectionExtension).Assembly;
                var applyInternalValidator = (noExternals) || (internalValidator && configuration.CustomImplementationMode != CustomImplementationMode.Replace);
                var applyExternalValidator = !internalValidator && (configuration.CustomImplementationMode == CustomImplementationMode.Combine || configuration.CustomImplementationMode == CustomImplementationMode.Replace);

                if (applyInternalValidator || applyExternalValidator)
                {
                    yield return validator;
                }
            }
        }
    }
}
