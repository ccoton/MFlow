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
    public class FluentValidation<T> : FluentConditions, IFluentValidation<T>
    {
        private readonly T _target;
        private readonly IPropertyNameResolver _resolver;
        public FluentValidation(T validate)
        {
            this.If(validate == null).Throw(new ArgumentException("validate"));
            _target = validate;
            _resolver = new PropertyNameResolver();
            base.Clear();
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

        public IFluentValidation<T> If(bool condition, string key = "", string message = "")
        {
            base.If(condition, key, message);
            return this;
        }

        public IFluentValidation<T> If(Expression<Func<T, bool>> expression, string message = "")
        {
            Func<T, bool> compiled = expression.Compile();
            return If(compiled.Invoke(_target), _resolver.Resolve<T, bool>(expression), message);
        }

        public IFluentValidation<T> And(bool condition, string key = "", string message = "")
        {
            base.And(condition, key, message);
            return this;
        }

        public IFluentValidation<T> And(Expression<Func<T, bool>> expression, string message = "")
        {
            Func<T, bool> compiled = expression.Compile();
            return And(compiled.Invoke(_target), _resolver.Resolve<T, bool>(expression), message);
        }

        public IFluentValidation<T> Or(bool condition, string key = "", string message = "")
        {
            base.Or(condition, key, message);
            return this;
        }

        public IFluentValidation<T> Or(Expression<Func<T, bool>> expression, string message = "")
        {
            Func<T, bool> compiled = expression.Compile();
            return Or(compiled.Invoke(_target), _resolver.Resolve<T, bool>(expression), message);
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

        public IFluentValidation<T> NotNullOrEmpty(Expression<Func<T, string>> expression, string message = "")
        {
            Func<T, string> compiled = expression.Compile();
            base.If(!string.IsNullOrEmpty(compiled.Invoke(_target)), _resolver.Resolve<T, string>(expression), message);
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
            foreach (var condition in base.Conditions.Where(c=>!c.Condition))
                results.Add(new ValidationResult<T>(condition));
            return results;
        }

        public new bool Satisfied()
        {
            return base.Satisfied();
        }
    }
}
