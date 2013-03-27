using MFlow.Core.ExpressionBuilder;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    public class CustomExpressionBuilder : IBuildExpressions
    {
        public System.Func<T, C> Compile<T, C>(System.Linq.Expressions.Expression<System.Func<T, C>> expression)
        {
            throw new System.NotImplementedException();
        }

        public C Invoke<T, C>(System.Func<T, C> compiled, T target)
        {
            throw new System.NotImplementedException();
        }
    }
}
