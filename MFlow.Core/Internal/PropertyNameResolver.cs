using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Internal
{
    /// <summary>
    ///     A class for resolving property names using expressions
    /// </summary>
    internal class PropertyNameResolver : IPropertyNameResolver
    {
        /// <summary>
        ///     Resolve a property name using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression)
        {
            LambdaExpression lambdaExpression = expression;
            MemberExpression memberExpression;

            if (lambdaExpression.Body is UnaryExpression)
            {
                memberExpression = (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand;
            }
            else if (lambdaExpression.Body is MemberExpression)
            {
                memberExpression = (MemberExpression)lambdaExpression.Body;
            }
            else
            {
                throw new Exception("Can only resolve a MemberExpression");
            }

            var expressionString = memberExpression.ToString();
            return expressionString.Substring(expressionString.IndexOf(".") + 1);
        }
    }
}
