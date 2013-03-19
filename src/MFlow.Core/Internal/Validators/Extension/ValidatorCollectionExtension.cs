using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Internal.Validators.Extension
{
    public static class ValidatorCollectionExtension
    {
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
    }
}
