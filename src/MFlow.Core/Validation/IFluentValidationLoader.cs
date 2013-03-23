namespace MFlow.Core.Validation
{
    /// <summary>
    ///     Load a fluentvalidation configuration
    /// </summary>
    public interface IFluentValidationLoader
    {
        /// <summary>
        ///     Load the configuration
        /// </summary>
        IFluentValidation<T> Load<T>(T target, string fileName = "") where T : class;
    }
}
