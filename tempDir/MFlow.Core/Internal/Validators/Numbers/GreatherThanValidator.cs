namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     GreaterThan Validator
    /// </summary>
    class GreaterThanValidator :  IGreaterThanValidator
    {
        public bool Validate(int input, int value)
        {
            return input > value;
        }
    }
}
