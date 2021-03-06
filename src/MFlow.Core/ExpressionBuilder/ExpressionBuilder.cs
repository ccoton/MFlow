﻿
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.ExpressionBuilder
{
    /// <summary>
    ///     An expression builder
    /// </summary>
    public class ExpressionBuilder : IBuildExpressions
    {
        /// <summary>
        ///     Compiles the expression
        /// </summary>
        public Func<T, C> Compile<T, C>(Expression<Func<T, C>> expression)
        {
            return expression.Compile();
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
