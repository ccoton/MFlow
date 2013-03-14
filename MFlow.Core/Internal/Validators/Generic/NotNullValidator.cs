namespace MFlow.Core.Internal.Validators.Generic
{
    /// <summary>
    ///     NotNullValidator Validator
    /// </summary>
    class NotNullValidator<T> : INotNullValidator<T>
    {
        public bool Validate (T input)
        {
            return input != null;
        }
    }
}
