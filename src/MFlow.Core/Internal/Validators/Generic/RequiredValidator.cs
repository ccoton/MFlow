using MFlow.Core.Validation.Validators.Generic;
namespace MFlow.Core.Internal.Validators.Generic
{
    /// <summary>
    ///     Required Validator
    /// </summary>
    class RequiredValidator<T> : IRequiredValidator<T>
    {
        public bool Validate (T input)
        {
            return input != null && !string.IsNullOrEmpty (input.ToString ()) && !input.Equals (default(T));
        }
    }
}
