using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Resources;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal
{
    /// <summary>
    ///     A class for resolving validation messages using expressions
    /// </summary>
    internal class MessageResolver : IMessageResolver
    {

        private readonly IPropertyNameResolver _propertyNameResolver;
        private readonly IResourceLocator _resourceLocator;

        public MessageResolver()
        {
            _propertyNameResolver = new PropertyNameResolver();
            _resourceLocator = new ResourceLocator();
        }


        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, ValidationType type, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }

            string outMessage = string.Empty;
            var propertyName = _propertyNameResolver.Resolve(expression);

            outMessage = string.Format(_resourceLocator.GetResource(type.ToString()), propertyName);

            return outMessage;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, O value, ValidationType type, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }

            string outMessage = string.Empty;
            var propertyName = _propertyNameResolver.Resolve(expression);

            outMessage = string.Format(_resourceLocator.GetResource(type.ToString()), propertyName, value.ToString());

            return outMessage;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, Expression<Func<T, O>> toExpression, ValidationType type, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }

            string outMessage = string.Empty;
            var propertyName = _propertyNameResolver.Resolve(expression);
            var toPropertyName = _propertyNameResolver.Resolve(toExpression);

            outMessage = string.Format(_resourceLocator.GetResource(type.ToString()), propertyName, toPropertyName.ToString());

            return outMessage;
        }
    }
}
