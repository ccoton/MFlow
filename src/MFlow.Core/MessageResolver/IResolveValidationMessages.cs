using System;
using System.Linq.Expressions;
using MFlow.Core.Validation.Enums;
using System.Collections.Generic;

namespace MFlow.Core.MessageResolver
{
    /// <summary>
    ///     A interface for resolving messages using expressions
    /// </summary>
    public interface IResolveValidationMessages
    {
        /// <summary>
        ///     Resolve a validation message using an property name
        /// </summary>
        string Resolve (string propertyName, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O> (Expression<Func<T, O>> expression, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O> (Expression<Func<T, O>> expression, Expression<Func<T, O>> toExpression, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O> (Expression<Func<T, O>> expression, O value, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O>(Expression<Func<T, ICollection<O>>> expression, O value, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O> (Expression<Func<T, O>> expression, O start, O end, ValidationType type, string message);
    }
}
