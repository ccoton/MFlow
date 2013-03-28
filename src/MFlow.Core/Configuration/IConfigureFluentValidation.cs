using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
using MFlow.Core.Statistics;
using MFlow.Core.Configuration.Enums;

namespace MFlow.Core.Configuration
{
    public interface IConfigureFluentValidation
    {
        CustomImplementationMode CustomImplementationMode { get; }
        IConfigureValidationStatistics Statistics { get;}
        IConfigureMessageResolver MessageResolver { get; }
        IConfigureExpressionBuilder ExpressionBuilder { get; }

        IConfigureFluentValidation WithDefaults();
        IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode);
        IConfigureFluentValidation WithStatistics(IConfigureValidationStatistics statisticsConfiguration);
        IConfigureFluentValidation WithoutStatistics();
        IConfigureFluentValidation WithMessageResolver(IConfigureMessageResolver messageResolverConfiguration);
        IConfigureFluentValidation WithExpressionBuilder(IConfigureExpressionBuilder expressionBuilderConfiguration);
    }
}
