namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     LessThanOrEqualTo Validator
    /// </summary>
    class LessThanOrEqualToValidator : ILessThanOrEqualToValidator
    {
        public bool Validate(int input, int value)
        {
            return input <= value;
        }
    }
}
