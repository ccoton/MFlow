namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     LessThanOrEqualTo Validator
    /// </summary>
    class LessThanOrEqualToValidator : IComparisonValidator<int, int>
    {
        public bool Validate(int input, int value)
        {
            return input <= value;
        }
    }
}
