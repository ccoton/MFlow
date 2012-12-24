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
        public IFluentValidation<T> NotNullOrEmpty(Expression<Func<T, string>> expression, string message = "")
        {
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target));
            base.If(derived, _resolver.Resolve<T, string>(expression), message);
            return this;
        }

        public IFluentValidation<T> Equal<C>(Expression<Func<T, C>> expression, C value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Equals(value);
            if (conditionType == ConditionType.And)
            {
                base.And(derived, _resolver.Resolve<T, C>(expression), message);
            }
            else
            {
                base.Or(derived, _resolver.Resolve<T, C>(expression), message);
            }
            return this;
        }

        public IFluentValidation<T> NotEqual<C>(Expression<Func<T, C>> expression, C value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !compiled.Invoke(_target).Equals(value);
            if (conditionType == ConditionType.And)
            {
                base.And(derived, _resolver.Resolve<T, C>(expression), message);
            }
            else
            {
                base.Or(derived, _resolver.Resolve<T, C>(expression), message);
            }
            return this;
        }

        public IFluentValidation<T> LessThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) < value;
            if (conditionType == ConditionType.And)
            {
                base.And(derived, _resolver.Resolve<T, int>(expression), message);
            }
            else
            {
                base.Or(derived, _resolver.Resolve<T, int>(expression), message);
            }
            return this;
        }

        public IFluentValidation<T> GreaterThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) > value;
            if (conditionType == ConditionType.And)
            {
                base.And(derived, _resolver.Resolve<T, int>(expression), message);
            }
            else
            {
                base.Or(derived, _resolver.Resolve<T, int>(expression), message);
            }
            return this;
        }

        public IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator)
        {
            base.And(validator.Satisfied());
            return this;
        }

        public IFluentValidation<T> RegEx(Expression<Func<T, string>> expression, string regEx, string message = "", ConditionType conditionType = ConditionType.And)
        {

            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            base.And(derived, _resolver.Resolve<T, string>(expression), message);

            return this;
        }
    }
}
