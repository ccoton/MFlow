namespace MFlow.Core.Internal.Validators
{
    /// <summary>
    ///     A validator
    /// </summary>
    interface IValidator<TInput>
    {
        /// <summary>
        ///     A validate method
        /// </summary>
        bool Validate (TInput input);
    }
}
