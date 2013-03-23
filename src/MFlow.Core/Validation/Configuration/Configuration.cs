
namespace MFlow.Core.Validation.Configuration
{
    public class Configuration
    {
        public static readonly IConfigureFluentValidation Current = new FluentValidationConfiguration();

        public IConfigureFluentValidation Clear()
        {
            Current.WithDefaults();
            return Current;
        }
    
    }
}
