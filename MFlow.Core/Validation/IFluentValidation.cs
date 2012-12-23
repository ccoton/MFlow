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
        IFluentValidation<T> If(bool condition, string key = "", string message = "");
        IFluentValidation<T> If(Expression<Func<T, bool>> expression, string message = "");
        IFluentValidation<T> And(bool condition, string key = "", string message = "");
        IFluentValidation<T> And(Expression<Func<T, bool>> expression, string message = "");
        IFluentValidation<T> Or(bool condition, string key = "", string message = "");
        IFluentValidation<T> Or(Expression<Func<T, bool>> expression, string message = "");
        IFluentValidation<T> Then(Action execute, ExecuteThread options = ExecuteThread.Current);
        IFluentValidation<T> Else(Action execute, ExecuteThread options = ExecuteThread.Current);
        IFluentValidation<T> Raise<E>(E eventToRaise) where E : IEvent<T>;

        IFluentValidation<T> NotNullOrEmpty(Expression<Func<T, string>> expression, string message = "");
        IFluentValidation<T> Equals<C>(Expression<Func<T, C>> expression, C value, string message = "");
        IFluentValidation<T> OrEquals<C>(Expression<Func<T, C>> expression, C value, string message = "");

        IEnumerable<IValidationResult<T>> Validate();
        void Throw<E>(E exception) where E : Exception;
        bool Satisfied();
    }
}
