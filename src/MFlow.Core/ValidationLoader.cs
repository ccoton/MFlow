using MFlow.Core.Validation;
using System;
using System.Linq.Expressions;

namespace MFlow.Core
{
    public class ValidationLoader
    {
        public IFluentValidationLoader Create<TLoader>()
        {
            var type = typeof(TLoader);
            var constructor = Expression.Lambda(Expression.New(type.GetConstructor(Type.EmptyTypes))).Compile();
            return (IFluentValidationLoader)constructor.DynamicInvoke();
        }
    }
}
