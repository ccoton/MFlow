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
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        private IList<Func<IFluentValidation<T>>> _dependencies = new List<Func<IFluentValidation<T>>>();

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression 
        /// </summary>
        public IFluentValidation<T> Equal<C>(Expression<Func<T, C>> expression, Expression<Func<T, C>> valueExpression, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Func<T, C> compiledValue = valueExpression.Compile();
            Expression<Func<T, bool>> derived = f => (compiled.Invoke(_target) != null && compiledValue.Invoke(_target) != null)
                && compiled.Invoke(_target).Equals(compiledValue.Invoke(_target));
            If(derived, _resolver.Resolve<T, C>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value 
        /// </summary>
        public IFluentValidation<T> Equal<C>(Expression<Func<T, C>> expression, C value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) != null && compiled.Invoke(_target).Equals(value);
            If(derived, _resolver.Resolve<T, C>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression 
        /// </summary>
        public IFluentValidation<T> NotEqual<C>(Expression<Func<T, C>> expression, Expression<Func<T, C>> valueExpression, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Func<T, C> compiledValue = valueExpression.Compile();
            Expression<Func<T, bool>> derived = f => (compiled.Invoke(_target) != null && compiledValue.Invoke(_target) != null) 
                && !compiled.Invoke(_target).Equals(compiledValue.Invoke(_target));
            If(derived, _resolver.Resolve<T, C>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value 
        /// </summary>
        public IFluentValidation<T> NotEqual<C>(Expression<Func<T, C>> expression, C value,  string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, C> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) != null && !compiled.Invoke(_target).Equals(value);
            If(derived, _resolver.Resolve<T, C>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        public IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator)
        {
            Expression<Func<T, bool>> derived = f => validator.Satisfied();
            base.And(derived);
            return this;
        }

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        public IFluentValidation<T> DependsOn<D>(Expression<Func<T, D>> validator, string message= "") where D : IFluentValidation<T>
        {
            Func<T, D> compiled = validator.Compile();
            _dependencies.Add(() => compiled.Invoke(_target));
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Satisfied();
            base.And(derived, message:message);
            return this;
        }
    }
}
