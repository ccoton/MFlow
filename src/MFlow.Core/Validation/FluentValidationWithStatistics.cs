using MEvents.Core;
using MFlow.Core.Conditions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Events;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Statistics;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public class FluentValidationWithStatistics<T> : FluentConditions<T>, IFluentValidation<T>, IFluentConditions<T>,
        IFluentValidationBuilder<T>
    {
        readonly IFluentValidation<T> _fluentValidation;
        readonly IRecordValidationStatistics _statistics;

        /// <summary>
        ///     Constructor
        /// </summary>
        internal FluentValidationWithStatistics(IFluentValidation<T> fluentValidation, IRecordValidationStatistics statistics, IBuildExpressions expressionBuilder) : 
            base(fluentValidation.GetTarget(), expressionBuilder)
        {
            if (fluentValidation == null)
                throw new ArgumentNullException("fluentValidation");

            if (statistics == null)
                throw new ArgumentNullException("statistics");

            _fluentValidation = fluentValidation;
            _statistics = statistics;
        }

        public IFluentValidationBuilder<T> SetTarget(T target)
        {
            return _fluentValidation.SetTarget(target);
        }

        public T GetTarget()
        {
            return _fluentValidation.GetTarget();
        }

        public IFluentValidationGeneric<T> Check<O>(Expression<Func<T, O>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public IFluentValidationString<T> Check(Expression<Func<T, string>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public IFluentValidationNumber<T> Check(Expression<Func<T, int>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public IFluentValidationNumber<T> Check(Expression<Func<T, int?>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public IFluentValidationDate<T> Check(Expression<Func<T, DateTime>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public IFluentValidationDate<T> Check(Expression<Func<T, DateTime?>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public IFluentValidationCollection<T> Check<O>(Expression<Func<T, ICollection<O>>> expression, ConditionType conditionType = ConditionType.And)
        {
            return _fluentValidation.Check(expression, conditionType);
        }

        public new IFluentValidation<T> Group(string name)
        {
            return _fluentValidation.Group(name);
        }

        public IFluentValidation<T> Warn()
        {
            return _fluentValidation.Warn();
        }

        public IFluentValidation<T> If(bool condition)
        {
            return _fluentValidation.If(condition);
        }

        public IFluentValidation<T> If(Expression<Func<T, bool>> expression)
        {
            return _fluentValidation.If(expression);
        }

        public IFluentValidation<T> And(bool condition)
        {
            return _fluentValidation.And(condition);
        }

        public IFluentValidation<T> And(Expression<Func<T, bool>> expression)
        {
            return _fluentValidation.And(expression);
        }

        public IFluentValidation<T> Or(bool condition)
        {
            return _fluentValidation.Or(condition);
        }

        public IFluentValidation<T> Or(Expression<Func<T, bool>> expression)
        {
            return _fluentValidation.Or(expression);
        }

        public IFluentValidation<T> Key(string key)
        {
            return _fluentValidation.Key(key);
        }

        public IFluentValidation<T> Message(string message)
        {
            return _fluentValidation.Message(message);
        }

        public IFluentValidation<T> Hint(string hint)
        {
            return _fluentValidation.Hint(hint);
        }

        public IFluentValidation<T> Reverse()
        {
            return _fluentValidation.Reverse();
        }

        public new IFluentValidation<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            return _fluentValidation.Then(execute, options);
        }

        public new IFluentValidation<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            return _fluentValidation.Else(execute, options);
        }

        public IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator)
        {
            return _fluentValidation.DependsOn(validator);
        }

        public IFluentValidation<T> DependsOn<D>(Expression<Func<T, D>> validator) where D : IFluentValidation<T>
        {
            return _fluentValidation.DependsOn(validator);
        }

        public IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>
        {
            return _fluentValidation.Raise(eventToRaise);
        }

        public new IFluentValidationBuilder<T> Clear()
        {
            return _fluentValidation.Clear();
        }

        public IEnumerable<IValidationResult<T>> Validate(string group = "", bool suppressWarnings = true)
        {
            return _statistics.RunAndRecord<T>(() => {return _fluentValidation.Validate(group, suppressWarnings).ToList();  });
        }

        public void Throw<E>(E exception) where E : Exception
        {
            _fluentValidation.Throw(exception);
        }

        public new bool Satisfied(string group = "", bool suppressWarnings = true)
        {
            return _fluentValidation.Satisfied(group, suppressWarnings);
        }
    }
}
