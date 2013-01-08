using System;
using System.Linq.Expressions;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal
{
    /// <summary>
    ///     A interface for resolving messages using expressions
    /// </summary>
    internal interface IMessageResolver
    {
        /// <summary>
        ///     Resolve a validation message using an property name
        /// </summary>
        string Resolve(string propertyName, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O>(Expression<Func<T, O>> expression, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O>(Expression<Func<T, O>> expression, Expression<Func<T, O>> toExpression, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O>(Expression<Func<T, O>> expression, O value, ValidationType type, string message);

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        string Resolve<T, O>(Expression<Func<T, O>> expression, O start, O end, ValidationType type, string message);
    }
}
