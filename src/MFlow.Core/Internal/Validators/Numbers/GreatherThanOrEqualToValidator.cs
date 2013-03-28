using MFlow.Core.Validation.Validators.Numbers;
namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     GreaterThanOrEqualTo Validator
    /// </summary>
    class GreaterThanOrEqualToValidator : IGreaterThanOrEqualToValidator
    {
        public bool Validate(int input, int value)
        {
            return input >= value;
        }
    }
}
