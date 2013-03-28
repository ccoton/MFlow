using MFlow.Core.Validation.Validators.Generic;
namespace MFlow.Core.Internal.Validators.Generic
{
    /// <summary>
    ///     EqualTo Validator
    /// </summary>
    class EqualToValidator<TInput, TCompare> : IEqualToValidator<TInput,TCompare>
    {
        public bool Validate(TInput input, TCompare value)
        {
            return input != null && input.Equals(value);
        }
    }
}
