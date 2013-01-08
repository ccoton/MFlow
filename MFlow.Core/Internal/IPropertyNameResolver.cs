using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal
{
    /// <summary>
    ///     A interface for resolving property names using expressions
    /// </summary>
    internal interface IPropertyNameResolver
    {
        /// <summary>
        ///     Resolve a property name using an expression
        /// </summary>
        string Resolve<T, O>(Expression<Func<T, O>> expression);
    }
}
