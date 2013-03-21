using MFlow.Core.Validation.Configuration.Enums;

namespace MFlow.Core.Validation.Configuration
{
    public interface IConfigureFluentValidation
    {
        CustomImplementationMode CustomImplementationMode { get; }

        IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode);
    }
}
