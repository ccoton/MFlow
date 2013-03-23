using MFlow.Core.Statistics;
using MFlow.Core.Validation.Configuration.Enums;

namespace MFlow.Core.Validation.Configuration
{
    public interface IConfigureFluentValidation
    {
        CustomImplementationMode CustomImplementationMode { get; }
        bool StatisticsEnabled { get; }
        IConfigureValidationStatistics StatisticsConfiguration { get;}

        IConfigureFluentValidation WithDefaults();
        IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode);
        IConfigureFluentValidation WithStatistics(IConfigureValidationStatistics statisticsConfiguration);
        IConfigureFluentValidation WithoutStatistics();
    }
}
