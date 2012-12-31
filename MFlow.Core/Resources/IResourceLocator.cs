using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
