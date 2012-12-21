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
    public interface IFluentValidation<T>
    {
        IFluentValidation<T> If(bool condition);
        IFluentValidation<T> If(Expression<Func<T, bool>> expression);
        IFluentValidation<T> And(bool condition);
        IFluentValidation<T> And(Expression<Func<T, bool>> expression);
        IFluentValidation<T> Or(bool condition);
        IFluentValidation<T> Or(Expression<Func<T, bool>> expression);
        IFluentValidation<T> Then(Action execute);
        IFluentValidation<T> Else(Action execute);
        IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>;
        void Throw<E>(E exception) where E : Exception;
        bool Satisfied();
    }
}
