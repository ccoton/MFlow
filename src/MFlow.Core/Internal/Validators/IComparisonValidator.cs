namespace MFlow.Core.Internal.Validators
{
    /// <summary>
    ///     A validator that compares values
    /// </summary>
    public interface IComparisonValidator<TInput, TCompare>
    {
        /// <summary>
        ///     A validate method
        /// </summary>
        bool Validate(TInput input, TCompare value);
    }
}
