using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;

namespace MFlow.Core.Validation
{
    public class FluentValidation<T> : FluentConditions, IFluentValidation<T>
    {
        private readonly T _target;

        public FluentValidation(T validate)
        {
            this.If(validate == null).Throw(new ArgumentException("validate"));
            _target = validate;
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

        public new IFluentValidation<T> If(bool condition)
        {
            base.If(condition);
            return this;
        }

        public IFluentValidation<T> If(Expression<Func<T, bool>> expression)
        {
            Func<T, bool> compiled = expression.Compile();
            return If(compiled.Invoke(_target));
        }

        public new IFluentValidation<T> And(bool condition)
        {
            base.And(condition);
            return this;
        }

        public IFluentValidation<T> And(Expression<Func<T, bool>> expression)
        {
            Func<T, bool> compiled = expression.Compile();
            return And(compiled.Invoke(_target));
        }

        public new IFluentValidation<T> Or(bool condition)
        {
            base.Or(condition);
            return this;
        }

        public IFluentValidation<T> Or(Expression<Func<T, bool>> expression)
        {
            Func<T, bool> compiled = expression.Compile();
            return Or(compiled.Invoke(_target));
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

        public new bool Satisfied()
        {
            return base.Satisfied();
        }
    }
}
