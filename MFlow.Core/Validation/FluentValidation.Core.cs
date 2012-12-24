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
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        private readonly IPropertyNameResolver _resolver;
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

        public void SetTarget(T target)
        {
            _target = target;
        }

        public void Throw<E>(E exception) where E : Exception
        {
            if (Satisfied())
            {
                base.Clear();
                throw exception;
            }
            base.Clear();
        }

        public new IFluentValidation<T> If(bool condition, string key = "", string message = "")
        {
            base.If(condition, key, message);
            return this;
        }

        public IFluentValidation<T> If(Expression<Func<T, bool>> expression, string message = "")
        {
            If(expression, _resolver.Resolve<T, bool>(expression), message);
            return this;
        }

        public new IFluentValidation<T> And(bool condition, string key = "", string message = "")
        {
            base.And(condition, key, message);
            return this;
        }

        public IFluentValidation<T> And(Expression<Func<T, bool>> expression, string message = "")
        {
            And(expression, _resolver.Resolve<T, bool>(expression), message);
            return this;
        }

        public new IFluentValidation<T> Or(bool condition, string key = "", string message = "")
        {
            base.Or(condition, key, message);
            return this;
        }

        public IFluentValidation<T> Or(Expression<Func<T, bool>> expression, string message = "")
        {
            Or(expression, _resolver.Resolve<T, bool>(expression), message);
            return this;
        }

        public new IFluentValidation<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            base.Then(execute, options);
            return this;
        }

        public new IFluentValidation<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current)
        {
            base.Else(execute, options);
            return this;
        }

        public IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>
        {
            var events = new EventsFactory().GetEventStore();
            events.Raise(eventToRaise);
            return this;
        }

        public IEnumerable<IValidationResult<T>> Validate()
        {
            var results = new List<IValidationResult<T>>();
            foreach (var condition in base.Conditions.Where(c => !c.Condition.Compile().Invoke(_target)))
                results.Add(new ValidationResult<T>(condition));
            return results;
        }

        public new bool Satisfied()
        {
            return base.Satisfied();
        }
    }
}
