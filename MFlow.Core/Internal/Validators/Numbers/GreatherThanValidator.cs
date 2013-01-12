namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     GreaterThan Validator
    /// </summary>
    class GreaterThanValidator : ICompareValidator<int, int>
    {
        public bool Validate (int input, int value)
        {
            return input > value;
        }
    }
}
