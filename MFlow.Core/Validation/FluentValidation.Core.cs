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
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        private readonly IPropertyNameResolver _resolver;

        /// <summary>
        ///     Constructor
        /// </summary>
        public FluentValidation(T validate)
            : base(validate)
        {
            this.If(validate == null).Throw(new ArgumentException("validate"));
            _resolver = new PropertyNameResolver();
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

        /// <summary>
        ///     Sets the target of this validation instance
        /// </summary>
        public void SetTarget(T target)
        {
            _target = target;
        }

        /// <summary>
        ///     Takes a boolean IF condition and evaluates it
        /// </summary>
        public new IFluentValidation<T> If(bool condition, string key = "", string message = "")
        {
            base.If(condition, key, message);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean IF condition, then evaluates it
        /// </summary>
        public IFluentValidation<T> If(Expression<Func<T, bool>> expression, string message = "")
        {
            If(expression, _resolver.Resolve<T, bool>(expression), message);
            return this;
        }

        /// <summary>
        ///     Takes a boolean AND condition and evaluates it
        /// </summary>
        public new IFluentValidation<T> And(bool condition, string key = "", string message = "")
        {
            base.And(condition, key, message);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean AND condition, then evaluates it
        /// </summary>
        public IFluentValidation<T> And(Expression<Func<T, bool>> expression, string message = "")
        {
            And(expression, _resolver.Resolve<T, bool>(expression), message);
            return this;
        }

        /// <summary>
        ///     Takes a boolean OR condition and evaluates it
        /// </summary>
        public new IFluentValidation<T> Or(bool condition, string key = "", string message = "")
        {
            base.Or(condition, key, message);
            return this;
        }

        /// <summary>
        ///     Takes an Expression and invokes it as a boolean OR condition, then evaluates it
        /// </summary>
        public IFluentValidation<T> Or(Expression<Func<T, bool>> expression, string message = "")
        {
            Or(expression, _resolver.Resolve<T, bool>(expression), message);
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
