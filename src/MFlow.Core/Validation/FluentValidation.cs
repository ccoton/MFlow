using MEvents.Core;
using MFlow.Core.Conditions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Events;
using MFlow.Core.Internal;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Configuration;
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
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>,
        IFluentValidationBuilder<T>
    {
        ICurrentValidationContext<T> _currentContext;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IValidatorFactory _validatorFactory;
        readonly IConvertValidatorToCondition<T> _validatorToCondition;
        readonly IEventCoordinator _eventCoordinator;
        readonly IConfigureFluentValidation _configuration;

        /// <summary>
        ///     Constructor
        /// </summary>
        internal FluentValidation(T validate, IPropertyNameResolver propertyNameResolver,
                                  IMessageResolver messageResolver, IExpressionBuilder<T> expressionBuilder,
                                  IValidatorFactory validatorFactory, IConvertValidatorToCondition<T> validatorToCondition,
                                  IEventCoordinator eventCoordinator, IConfigureFluentValidation configuration)
            : base(validate)
        {
            If(validate == null).Throw(new ArgumentException("validate"));
            If(propertyNameResolver == null).Throw(new ArgumentException("propertyNameResolver"));
            If(messageResolver == null).Throw(new ArgumentException("messageResolver"));
            If(expressionBuilder == null).Throw(new ArgumentException("expressionBuilder"));
            If(validatorFactory == null).Throw(new ArgumentException("validatorFactory"));
            If(validatorToCondition == null).Throw(new ArgumentException("validatorToCondition"));
            If(eventCoordinator == null).Throw(new ArgumentException("eventCoordinator"));
            If(configuration == null).Throw(new ArgumentException("configuration"));

            _propertyNameResolver = propertyNameResolver;
            _messageResolver = messageResolver;
            _expressionBuilder = expressionBuilder;
            _validatorFactory = validatorFactory;
            _validatorToCondition = validatorToCondition;
            _eventCoordinator = eventCoordinator;
            _configuration = configuration;

            base.Clear();
        }

        IFluentValidation<T> BuildIf(Expression<Func<T, bool>> expression, string key, string message)
        {
            if (_currentContext.ConditionType == ConditionType.And)
                And(expression, key, message, "", _currentContext.ConditionOutput);
            else
                Or(expression, key, message, "", _currentContext.ConditionOutput);
            return this;
        }

        /// <summary>
        ///     Sets the target of this validation instance
        /// </summary>
        public IFluentValidationBuilder<T> SetTarget(T target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            _target = target;
            return this;
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
        public IFluentValidationGeneric<T> Check<O>(Expression<Func<T, O>> expression, ConditionType conditionType = ConditionType.And)
        {
            _currentContext = new CurrentValidationContext<T>(expression, conditionType, ConditionOutput.Error, (Nullable.GetUnderlyingType(typeof(O)) != null));
            return this;
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidationString<T> Check(Expression<Func<T, string>> expression, ConditionType conditionType = ConditionType.And)
        {
            return (IFluentValidationString<T>)Check<string>(expression, conditionType);
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidationNumber<T> Check(Expression<Func<T, int>> expression, ConditionType conditionType = ConditionType.And)
        {
            return (IFluentValidationNumber<T>)Check<int>(expression, conditionType);
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidationNumber<T> Check(Expression<Func<T, int?>> expression, ConditionType conditionType = ConditionType.And)
        {
            return (IFluentValidationNumber<T>)Check<int?>(expression, conditionType);
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidationDate<T> Check(Expression<Func<T, DateTime>> expression, ConditionType conditionType = ConditionType.And)
        {
            return (IFluentValidationDate<T>)Check<DateTime>(expression, conditionType);
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidationDate<T> Check(Expression<Func<T, DateTime?>> expression, ConditionType conditionType = ConditionType.And)
        {
            return (IFluentValidationDate<T>)Check<DateTime?>(expression, conditionType);
        }

        /// <summary>
        ///     Adds an expression to the chain 
        /// </summary>
        public IFluentValidationCollection<T> Check<O>(Expression<Func<T, ICollection<O>>> expression, ConditionType conditionType = ConditionType.And)
        {
            return (IFluentValidationCollection<T>)Check<ICollection<O>>(expression, conditionType);
        }

        /// <summary>
        ///     Group a set of validation checks into a set
        /// </summary>
        public new IFluentValidation<T> Group(string name)
        {
            if (_currentContext == null)
                throw new ApplicationException("Calling Group on a validator is only valid once the validator has at least one context, i.e. Check has been called");

            base.Group(name);

            return this;
        }

        /// <summary>
        ///     When applied to a Check make it behave as a warning, by default will not be raised when validation occurs
        /// </summary>
        public IFluentValidation<T> Warn()
        {
            Conditions.Last().SetConditionOutput(ConditionOutput.Warning);
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
            If(expression, _propertyNameResolver.Resolve<T, bool>(expression));
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
            And(expression, _propertyNameResolver.Resolve<T, bool>(expression));
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
            Or(expression, _propertyNameResolver.Resolve<T, bool>(expression));
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
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        public IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator)
        {
            Expression<Func<T, bool>> derived = f => validator.Satisfied(string.Empty, true);
            base.And(derived);
            return this;
        }

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        public IFluentValidation<T> DependsOn<D>(Expression<Func<T, D>> validator) where D : IFluentValidation<T>
        {
            Func<T, D> compiled = _expressionBuilder.Compile(validator);
            _dependencies.Add(() => compiled.Invoke(_target));
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Satisfied(string.Empty, true);
            base.And(derived, message: string.Empty);
            return this;
        }

        /// <summary>
        ///     Add a message to a validation expression
        /// </summary>
        public IFluentValidation<T> Message(string message)
        {
            if (Conditions.Any())
            {
                var lastCondition = Conditions.Last();
                message = _messageResolver.Resolve(lastCondition.Key, ValidationType.Unknown, message);
                lastCondition.SetMessage(message);
            }
            return this;
        }

        /// <summary>
        ///     Add a hint to a validation expression
        /// </summary>
        public IFluentValidation<T> Hint(string hint)
        {
            if (Conditions.Any())
            {
                var lastCondition = Conditions.Last();
                hint = _messageResolver.Resolve(lastCondition.Key, ValidationType.Unknown, hint);
                lastCondition.SetHint(hint);
            }
            return this;
        }

        /// <summary>
        ///     Takes the condition on the current context and reverses it
        /// </summary>
        public IFluentValidation<T> Reverse()
        {
            if (Conditions.Any())
            {
                var lastCondition = Conditions.Last();
                Func<T, bool> compiled = _expressionBuilder.Compile(lastCondition.Condition);
                Expression<Func<T, bool>> reversed = f => !compiled.Invoke(_target);
                Conditions.Remove(lastCondition);
                BuildIf(reversed, lastCondition.Key, lastCondition.Message);
            }
            return this;
        }

        /// <summary>
        ///     Add a key to a validation expression
        /// </summary>
        public IFluentValidation<T> Key(string key)
        {
            if (Conditions.Any())
                Conditions.Last().SetKey(key);
            return this;
        }

        /// <summary>
        ///     Clears the validator
        /// </summary>
        public new IFluentValidationBuilder<T> Clear()
        {
            base.Clear();
            return this;
        }

        /// <summary>
        ///     Raises an event
        /// </summary>
        public IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>
        {
            var events = new EventsFactory().GetEventCoordinator();
            events.Publish(eventToRaise);
            return this;
        }

        /// <summary>
        ///     Validate this instance
        /// </summary>
        public IEnumerable<IValidationResult<T>> Validate(string group = "", bool suppressWarnings = true)
        {
            var results = new List<IValidationResult<T>>();

            foreach (var condition in Conditions.Where(c => c.GroupName.ToLower() == group.ToLower() || string.IsNullOrEmpty(group)).ToList()
                .Where(c => (c.Output == ConditionOutput.Error) || (c.Output == ConditionOutput.Warning && !suppressWarnings))
                .Where(c => !c.Condition.Compile().Invoke(_target)))
                results.Add(new ValidationResult<T>(condition));

            foreach (var dependency in _dependencies)
                results.AddRange(dependency.Invoke().Validate());

            if (results.Any())
                _eventCoordinator.Publish(new ValidationFailedEvent<T>(this));
            else
                _eventCoordinator.Publish(new ValidatedEvent<T>(this));

            return results;
        }

        /// <summary>
        ///     Throws an exception when the validation instance is not satisfied
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
        public new bool Satisfied(string group = "", bool suppressWarnings = true)
        {
            return base.Satisfied(group, suppressWarnings);
        }
    }
}
