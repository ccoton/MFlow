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

        public IFluentValidation<T> Equals<C>(Expression<Func<T, C>> expression, C value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            if (conditionType == ConditionType.And)
            {
                base.And(compiled.Invoke(_target).Equals(value), _resolver.Resolve<T, C>(expression), message);
            }
            else
            {
                base.Or(compiled.Invoke(_target).Equals(value), _resolver.Resolve<T, C>(expression), message);
            }
            return this;
        }

        public IFluentValidation<T> LessThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            if (conditionType == ConditionType.And)
            {
                base.And(compiled.Invoke(_target) < value, _resolver.Resolve<T, int>(expression), message);
            }
            else
            {
                base.Or(compiled.Invoke(_target) < value, _resolver.Resolve<T, int>(expression), message);
            }
            return this;
        }

        public IFluentValidation<T> GreaterThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            if (conditionType == ConditionType.And)
            {
                base.And(compiled.Invoke(_target) > value, _resolver.Resolve<T, int>(expression), message);
            }
            else
            {
                base.Or(compiled.Invoke(_target) > value, _resolver.Resolve<T, int>(expression), message);
            }
            return this;
        }
    }
}
