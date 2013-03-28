
using System;
using System.Linq.Expressions;

namespace MFlow.Core.ExpressionBuilder
{
    /// <summary>
    ///     An expression builder interface
    /// </summary>
    public interface IBuildExpressions
    {
        /// <summary>
        ///     Compiles the expression
        /// </summary>
        Func<T, C> Compile<T, C> (Expression<Func<T, C>> expression);

        /// <summary>
        ///     Invokes the expression
        /// </summary>
        C Invoke<T, C> (Func<T, C> compiled, T target);
    }
}
