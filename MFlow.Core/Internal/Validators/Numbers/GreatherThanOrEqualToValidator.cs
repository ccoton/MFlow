namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     GreaterThanOrEqualTo Validator
    /// </summary>
    class GreaterThanOrEqualToValidator : IComparisonValidator<int, int>
    {
        public bool Validate(int input, int value)
        {
            return input >= value;
        }
    }
}
