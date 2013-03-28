
using MFlow.Core.Configuration;
namespace MFlow.Core
{
    public class MFlowConfiguration
    {
        public static readonly IConfigureFluentValidation Current = new FluentValidationConfiguration();

        public IConfigureFluentValidation Clear()
        {
            Current.WithDefaults();
            return Current;
        }
    
    }
}
