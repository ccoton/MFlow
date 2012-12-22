using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Internal
{
    internal interface IPropertyNameResolver
    {
        string Resolve<T, O>(Expression<Func<T, O>> expression);
    }
}
