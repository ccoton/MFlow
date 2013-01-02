using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.XmlConfiguration;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A factory to provide an fluentvalidation implementation
    /// </summary>
    public class FluentValidationFactory : IFluentValidationFactory
    {
        /// <summary>
        ///     Gets a fluent validation implementation
        /// </summary>
        public IFluentCreator<T> GetFluentValidation<T>(T target, bool loadXmlRuleset = false, string fileName = "")
        {
            if(!loadXmlRuleset)
                return new FluentValidation<T>(target);
            IFluentValidationLoader loader = null; 
            if(fileName.ToLower().EndsWith(".vml"))
                loader = new VmlValidationLoader();
            else 
                loader = new XmlValidationLoader();

            return (IFluentCreator<T>)(string.IsNullOrEmpty(fileName) ? loader.Load<T>(target) : loader.Load<T>(target, fileName));
        }
    }
}
