
using MFlow.Core.Internal.Validators;

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
        IValidator<T> GetValidator<T, ValidatorT>() where ValidatorT : IValidator<T>;

        /// <summary>
        ///     Gets the validator.
        /// </summary>
        IComparisonValidator<TInput, TCompare> GetValidator<TInput, TCompare, ValidatorT>() where ValidatorT : IComparisonValidator<TInput, TCompare>;
    }
}

