using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Internal
{
    internal class PropertyNameResolver : IPropertyNameResolver
    {
        public string Resolve<T, O>(Expression<Func<T, O>> expression)
        {
            LambdaExpression lambdaExpression = expression;
            MemberExpression memberExpression;

            if (lambdaExpression.Body is UnaryExpression)
                memberExpression = (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand;
            else
                memberExpression = (MemberExpression)lambdaExpression.Body;

            var expressionString = memberExpression.ToString();

            return expressionString.Substring(expressionString.IndexOf(".") + 1);
        }
    }
}
