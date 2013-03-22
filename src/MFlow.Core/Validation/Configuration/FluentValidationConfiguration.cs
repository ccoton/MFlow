using MFlow.Core.Statistics;
using MFlow.Core.Validation.Configuration.Enums;

namespace MFlow.Core.Validation.Configuration
{
    public class FluentValidationConfiguration : IConfigureFluentValidation
    {
        internal FluentValidationConfiguration()
        {
            WithDefaults();
        }

        public IConfigureValidationStatistics StatisticsConfiguration { get; private set; }
        public CustomImplementationMode CustomImplementationMode { get; private set; }
        public bool StatisticsEnabled { get; private set; }

        public IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode)
        {
            CustomImplementationMode = mode;
            return this;
        }

        public IConfigureFluentValidation WithStatistics(IConfigureValidationStatistics statisticsConfiguration)
        {
            StatisticsConfiguration = statisticsConfiguration;
            StatisticsEnabled = true;
            return this;
        }

        public IConfigureFluentValidation WithoutStatistics()
        {
            StatisticsEnabled = false;
            return this;
        }

        public IConfigureFluentValidation WithDefaults()
        {
            CustomImplementationMode = Enums.CustomImplementationMode.Ignore;
            StatisticsConfiguration = new StatisticsConfiguration(new NullValidationStatisticsRecorder());
            StatisticsEnabled = true;
            return this;
        }
    }
}
