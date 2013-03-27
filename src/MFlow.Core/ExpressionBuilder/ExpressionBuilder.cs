
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.ExpressionBuilder
{
    /// <summary>
    ///     An expression builder
    /// </summary>
    class ExpressionBuilder : IBuildExpressions
    {
        static readonly IDictionary<object, object> _expressions = new Dictionary<object, object>();
        object _expressionsLock = new object();

        /// <summary>
        ///     Compiles the expression
        /// </summary>
        public Func<T, C> Compile<T, C>(Expression<Func<T, C>> expression)
        {
            lock (_expressionsLock)
            {
                if (!_expressions.ContainsKey(expression))
                {
                    var compiled = expression.Compile();
                    _expressions.Add(expression, compiled);
                }
            }
            return (Func<T, C>)_expressions [expression];
        }

        /// <summary>
        ///     Invokes the expression
        /// </summary>
        public C Invoke<T, C>(Func<T, C> compiled, T target)
        {
            return compiled.Invoke(target);
        }
    }
}
