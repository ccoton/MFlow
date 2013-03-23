
using MFlow.Core.Internal.Validators;
using System.Collections.Generic;

namespace MFlow.Core
{
    /// <summary>
    ///     An interface defining a factory that returns validators
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        ///     Gets the validator.
        /// </summary>
        ICollection<IValidator<T>> GetValidators<T, ValidatorT>() where ValidatorT : IValidator<T>;

        /// <summary>
        ///     Gets the validator.
        /// </summary>
        ICollection<IComparisonValidator<TInput, TCompare>> GetValidators<TInput, TCompare, ValidatorT>() where ValidatorT : IComparisonValidator<TInput, TCompare>;
    }
}

