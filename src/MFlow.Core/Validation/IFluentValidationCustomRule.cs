using System;

namespace MFlow.Core.Validation
{
    public interface IFluentValidationCustomRule<T> 
    {
        IFluentValidation<T> Execute(Func<T> targetFunc);
    }
}
