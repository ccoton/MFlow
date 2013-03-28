namespace MFlow.Core.Validation.Validators
{
    /// <summary>
    ///     A validator that compares values
    /// </summary>
    public interface IComparisonValidator<TInput, TCompare> : ICanValidate
    {
        /// <summary>
        ///     A validate method
        /// </summary>
        bool Validate(TInput input, TCompare value);
    }
}
