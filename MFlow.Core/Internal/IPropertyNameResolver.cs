using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
