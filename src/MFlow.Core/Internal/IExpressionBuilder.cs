
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal
{
    /// <summary>
    ///     An expression builder interface
    /// </summary>
    public interface IExpressionBuilder<T>
    {
        /// <summary>
        ///     Compiles the expression
        /// </summary>
        Func<T, C> Compile<C> (Expression<Func<T, C>> expression);

        /// <summary>
        ///     Invokes the expression
        /// </summary>
        C Invoke<C> (Func<T, C> compiled, T target);
    }
}
