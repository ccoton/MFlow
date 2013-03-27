using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
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
        public IConfigureMessageResolver MessageResolverConfiguration { get; private set; }
        public CustomImplementationMode CustomImplementationMode { get; private set; }
        public IConfigureExpressionBuilder ExpressionBuilderConfiguration { get; private set; }
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
            MessageResolverConfiguration = new MessageResolverConfiguration(new MessageResolver.MessageResolver());
            ExpressionBuilderConfiguration = new ExpressionBuilderConfiguration(new ExpressionBuilder.ExpressionBuilder());
            StatisticsEnabled = true;
            return this;
        }

        public IConfigureFluentValidation WithMessageResolver(IConfigureMessageResolver messageResolverConfiguration)
        {
            MessageResolverConfiguration = messageResolverConfiguration;
            return this;
        }

        public IConfigureFluentValidation WithExpressionBuilder(IConfigureExpressionBuilder expressionBuilderConfiguration)
        {
            ExpressionBuilderConfiguration = expressionBuilderConfiguration;
            return this;
        }
    }
}
