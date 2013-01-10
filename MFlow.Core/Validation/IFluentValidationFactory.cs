namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A factory to provide an fluentvalidation implementation
    /// </summary>
    public interface IFluentValidationFactory
    {
        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        IFluentValidationBuilder<T> GetFluentValidation<T>(T target, bool loadXmlRuleset = false, 
                                                           string fileName = "");
    }
}
