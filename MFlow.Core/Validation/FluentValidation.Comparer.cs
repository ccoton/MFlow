using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Internal;

namespace MFlow.Core.Validation
{
    public partial class FluentValidation<T> : FluentConditions, IFluentValidation<T>
    {
        public IFluentValidation<T> NotNullOrEmpty(Expression<Func<T, string>> expression, string message = "")
        {
            Func<T, string> compiled = expression.Compile();
            base.If(!string.IsNullOrEmpty(compiled.Invoke(_target)), _resolver.Resolve<T, string>(expression), message);
            return this;
        }

        public IFluentValidation<T> Equals<C>(Expression<Func<T, C>> expression, C value, string message = "")
        {
            Func<T, C> compiled = expression.Compile();
            base.If(compiled.Invoke(_target).Equals(value), _resolver.Resolve<T, C>(expression), message);
            return this;
        }

        public IFluentValidation<T> OrEquals<C>(Expression<Func<T, C>> expression, C value, string message = "")
        {
            Func<T, C> compiled = expression.Compile();
            base.Or(compiled.Invoke(_target).Equals(value), _resolver.Resolve<T, C>(expression), message);
            return this;
        }
    }
}
