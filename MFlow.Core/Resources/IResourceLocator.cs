namespace MFlow.Core.Resources
{
    /// <summary>
    ///     A resource locator
    /// </summary>
    internal interface IResourceLocator
    {
        /// <summary>
        ///     Gets a resource by its key
        /// </summary>
        string GetResource(string key);
    }
}
