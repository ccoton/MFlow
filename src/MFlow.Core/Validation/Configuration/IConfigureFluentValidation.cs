using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
using MFlow.Core.Statistics;
using MFlow.Core.Validation.Configuration.Enums;

namespace MFlow.Core.Validation.Configuration
{
    public interface IConfigureFluentValidation
    {
        CustomImplementationMode CustomImplementationMode { get; }
        bool StatisticsEnabled { get; }
        IConfigureValidationStatistics StatisticsConfiguration { get;}
        IConfigureMessageResolver MessageResolverConfiguration { get; }
        IConfigureExpressionBuilder ExpressionBuilderConfiguration { get; }

        IConfigureFluentValidation WithDefaults();
        IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode);
        IConfigureFluentValidation WithStatistics(IConfigureValidationStatistics statisticsConfiguration);
        IConfigureFluentValidation WithoutStatistics();
        IConfigureFluentValidation WithMessageResolver(IConfigureMessageResolver messageResolverConfiguration);
        IConfigureFluentValidation WithExpressionBuilder(IConfigureExpressionBuilder expressionBuilderConfiguration);
    }
}
