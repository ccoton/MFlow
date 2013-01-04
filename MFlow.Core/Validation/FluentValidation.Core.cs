using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Internal;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationBuilder<T>
    {
        private readonly IPropertyNameResolver _resolver;
        private readonly IMessageResolver _messageResolver;
        private object _currentExpression;

        /// <summary>
        ///     Constructor
        /// </summary>
        internal FluentValidation(T validate)
            : base(validate)
        {
            this.If(validate == null).Throw(new ArgumentException("validate"));
            _resolver = new PropertyNameResolver();
            _messageResolver = new MessageResolver();
            base.Clear();
        }

        private IFluentValidation<T> If(Expression<Func<T, bool>> expression, string key, string message, ConditionType conditionType = ConditionType.And)
        {
            if (conditionType == ConditionType.And)
                And(expression, key, message);
            else
                Or(expression, key, message);
            return this;
        }

        private Expression<Func<T, C>> GetCurrentExpression<C>() 
        {
            return (Expression<Func<T, C>>)_currentExpression;
        }

        private void SetCurrentExpression<C>(Expression<Func<T, C>> expression)
        {
            _currentExpression= expression; 
        }

        /// <summary>
        ///     Sets the target of this validation instance
        /// </summary>
        public void SetTarget(T target)
        {
            _target = target;
        }

        /// <summary>
        ///     Gets the target of this validation instance
        /// </summary>
        public T GetTarget()
        {
            return _target;
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidation<T> Check<O>(Expression<Func<T, O>> expression, ConditionType conditionType = ConditionType.And)
        {
            SetCurrentExpression(expression);
            return this;
        }

        /// <summary>
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        public IFluentValidation<T> If(bool condition)
        {
            base.If(condition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean IF condition, then evaluates it
        /// </summary>
        public IFluentValidation<T> If(Expression<Func<T, bool>> expression)
        {
            If(expression, _resolver.Resolve<T, bool>(expression));
            return this;
        }

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        public IFluentValidation<T> And(bool condition)
        {
            base.And(condition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        public IFluentValidation<T> And(Expression<Func<T, bool>> expression)
        {
            And(expression, _resolver.Resolve<T, bool>(expression));
            return this;
        }

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        public IFluentValidation<T> Or(bool condition)
        {
            base.Or(condition);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        public IFluentValidation<T> Or(Expression<Func<T, bool>> expression)
        {
            Or(expression, _resolver.Resolve<T, bool>(expression));
            return this;
        }

        /// <summary>
        ///     Takes an action to execute if the validator is satisfied
        /// </summary>
        public new IFluentValidation<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            base.Then(execute, options);
            return this;
        }

        /// <summary>
        ///     Takes an action to execute if the validator is not satisfied
        /// </summary>
        public new IFluentValidation<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            base.Else(execute, options);
            return this;
        }

        /// <summary>
        ///     Add a message to a validation expression
        /// </summary>
        public IFluentValidation<T> Message(string message)
        {
            if (_conditions.Any())
            {
                var lastCondition = _conditions.Last();
                message = _messageResolver.Resolve(lastCondition.Key, ValidationType.Unknown, message);
                lastCondition.SetMessage(message);
            }
            return this;
        }

        /// <summary>
        ///     Add a key to a validation expression
        /// </summary>
        public IFluentValidation<T> Key(string key)
        {
            if (_conditions.Any())
                _conditions.Last().SetKey(key);
            return this;
        }

        /// <summary>
        ///     Clears the validator
        /// </summary>
        public new IFluentValidation<T> Clear()
        {
            base.Clear();
            return this;
        }

        /// <summary>
        ///     Raises an event
        /// </summary>
        public IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>
        {
            var events = new EventsFactory().GetEventStore();
            events.Raise(eventToRaise);
            return this;
        }

        /// <summary>
        ///     Validate this instance
        /// </summary>
        public IEnumerable<IValidationResult<T>> Validate()
        {
            var results = new List<IValidationResult<T>>();

            foreach (var condition in base._conditions.Where(c => !c.Condition.Compile().Invoke(_target)))
                results.Add(new ValidationResult<T>(condition));

            foreach (var dependency in _dependencies)
                results.AddRange(dependency.Invoke().Validate());

            return results;
        }

        /// <summary>
        ///     Validate this instance
        /// </summary>
        public IEnumerable<IValidationResult<T>> ValidateAndThrow<E>() where E : Exception, new()
        {
            var results = Validate();
            if (results.Any())
                throw new E();
            return results;
        }

        /// <summary>
        ///     Throws an exception
        /// </summary>
        public void Throw<E>(E exception) where E : Exception
        {
            if (Satisfied())
            {
                base.Clear();
                throw exception;
            }
            base.Clear();
        }

        /// <summary>
        ///     Returns a boolean indicating if this validator is satisfied
        /// </summary>
        public new bool Satisfied()
        {
            return base.Satisfied();
        }
    }
}
