using MFlow.Core.Validation.Validators.Numbers;
namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     LessThan Validator
    /// </summary>
    class LessThanValidator : ILessThanValidator
    {
        public bool Validate(int input, int value)
        {
            return input < value;
        }
    }
}
