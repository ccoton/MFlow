using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
using MFlow.Core.Statistics;
using MFlow.Core.Configuration.Enums;

namespace MFlow.Core.Configuration
{
    public class FluentValidationConfiguration : IConfigureFluentValidation
    {
        internal FluentValidationConfiguration()
        {
            WithDefaults();
        }

        public IConfigureValidationStatistics Statistics { get; private set; }
        public IConfigureMessageResolver MessageResolver { get; private set; }
        public IConfigureExpressionBuilder ExpressionBuilder { get; private set; }

        public CustomImplementationMode CustomImplementationMode { get; private set; }

        /// <summary>
        ///    Set the custom implementation mode
        /// </summary>
        public IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode)
        {
            CustomImplementationMode = mode;
            return this;
        }

        /// <summary>
        ///    Sets the statistics configuration
        /// </summary>
        public IConfigureFluentValidation WithStatistics(IConfigureValidationStatistics statisticsConfiguration)
        {
            Statistics = statisticsConfiguration;
            Statistics.Enable();
            return this;
        }

        /// <summary>
        ///    Turns statistics off
        /// </summary>
        public IConfigureFluentValidation WithoutStatistics()
        {
            Statistics.Disable();
            return this;
        }

        /// <summary>
        ///     Sets the message resolver configuration
        /// </summary>
        public IConfigureFluentValidation WithMessageResolver(IConfigureMessageResolver messageResolverConfiguration)
        {
            MessageResolver = messageResolverConfiguration;
            return this;
        }

        /// <summary>
        ///    Sets the expression builder configuration
        /// </summary>
        public IConfigureFluentValidation WithExpressionBuilder(IConfigureExpressionBuilder expressionBuilderConfiguration)
        {
            ExpressionBuilder = expressionBuilderConfiguration;
            return this;
        }

        /// <summary>
        ///     Sets the default configuration
        /// </summary>
        public IConfigureFluentValidation WithDefaults()
        {
            CustomImplementationMode = Enums.CustomImplementationMode.Ignore;
            Statistics = new StatisticsConfiguration(new NullValidationStatisticsRecorder()).Enable();
            MessageResolver = new MessageResolverConfiguration(new MessageResolver.MessageResolver());
            ExpressionBuilder = new ExpressionBuilderConfiguration(new ExpressionBuilder.CachingExpressionBuilder());
            return this;
        }
    }
}
