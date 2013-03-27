using MFlow.Core.Internal;
using MFlow.Core.MessageResolver;
using System;
using System.Collections.Generic;

namespace MFlow.Core.Tests.for_FluentValidationConfiguration
{
    public class CustomMessageResolver : IResolveValidationMessages
    {
        public string Resolve(string propertyName, Validation.Enums.ValidationType type, string message)
        {
            throw new NotImplementedException();
        }

        public string Resolve<T, O>(System.Linq.Expressions.Expression<Func<T, O>> expression, Validation.Enums.ValidationType type, string message)
        {
            throw new NotImplementedException();
        }

        public string Resolve<T, O>(System.Linq.Expressions.Expression<Func<T, O>> expression, System.Linq.Expressions.Expression<Func<T, O>> toExpression, Validation.Enums.ValidationType type, string message)
        {
            throw new NotImplementedException();
        }

        public string Resolve<T, O>(System.Linq.Expressions.Expression<Func<T, O>> expression, O value, Validation.Enums.ValidationType type, string message)
        {
            throw new NotImplementedException();
        }

        public string Resolve<T, O>(System.Linq.Expressions.Expression<Func<T, ICollection<O>>> expression, O value, Validation.Enums.ValidationType type, string message)
        {
            throw new NotImplementedException();
        }

        public string Resolve<T, O>(System.Linq.Expressions.Expression<Func<T, O>> expression, O start, O end, Validation.Enums.ValidationType type, string message)
        {
            throw new NotImplementedException();
        }
    }
}
