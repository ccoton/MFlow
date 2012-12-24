using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Internal;

namespace MFlow.Core.Validation
{
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        public IFluentValidation<T> Equal<C>(Expression<Func<T, C>> expression, C value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) != null && compiled.Invoke(_target).Equals(value);
            If(derived, _resolver.Resolve<T, C>(expression), message, conditionType);
            return this;
        }

        public IFluentValidation<T> NotEqual<C>(Expression<Func<T, C>> expression, C value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) != null && !compiled.Invoke(_target).Equals(value);
            If(derived, _resolver.Resolve<T, C>(expression), message, conditionType);
            return this;
        }

        public IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator)
        {
            base.And(validator.Satisfied());
            return this;
        }
    }
}
