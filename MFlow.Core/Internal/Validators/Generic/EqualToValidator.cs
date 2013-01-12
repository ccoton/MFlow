namespace MFlow.Core.Internal.Validators.Generic
{
    /// <summary>
    ///     EqualTo Validator
    /// </summary>
    class EqualToValidator<T, T2> : IComparisonValidator<T, T2>
    {
        public bool Validate(T input, T2 value)
        {
            return input != null && input.Equals(value);
        }
    }
}
