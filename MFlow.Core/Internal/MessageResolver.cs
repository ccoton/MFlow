using System;
using System.Linq.Expressions;
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
        ///     Resolve a validation message using an property name
        /// </summary>
        public string Resolve(string propertyName, ValidationType type, string message)
        {
            if (ShouldResolve(message))
            {
                var customMessage = message.StartsWith("$") && message.EndsWith("$");

                if (!customMessage && type == ValidationType.Unknown)
                    return string.Empty;

                message = PrepareForResolve(message);
                var key = customMessage ? message : type.ToString();

                string outMessage = string.Empty;

                outMessage = string.Format(_resourceLocator.GetResource(key), propertyName);

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, ValidationType type, string message)
        {
            if (ShouldResolve(message))
            {
                var customMessage = message.StartsWith("$") && message.EndsWith("$");
                message = PrepareForResolve(message);
                var key = customMessage ? message : type.ToString();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve(expression);

                outMessage = string.Format(_resourceLocator.GetResource(key), propertyName);

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, O value, ValidationType type, string message)
        {
            if (ShouldResolve(message))
            {
                var customMessage = message.StartsWith("$") && message.EndsWith("$");
                message = PrepareForResolve(message);
                var key = customMessage ? message : type.ToString();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve(expression);

                outMessage = string.Format(_resourceLocator.GetResource(key), propertyName, value != null ? value.ToString() : "null");

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, Expression<Func<T, O>> toExpression, ValidationType type, string message)
        {
            if (ShouldResolve(message))
            {
                var customMessage = message.StartsWith("$") && message.EndsWith("$");
                message = PrepareForResolve(message);
                var key = customMessage ? message : type.ToString();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve(expression);
                var toPropertyName = _propertyNameResolver.Resolve(toExpression);

                outMessage = string.Format(_resourceLocator.GetResource(key), propertyName, toPropertyName.ToString());

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O>(Expression<Func<T, O>> expression, O start, O end, ValidationType type, string message)
        {
            if (ShouldResolve(message))
            {
                var customMessage = message.StartsWith("$") && message.EndsWith("$");
                message = PrepareForResolve(message);
                var key = customMessage ? message : type.ToString();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve(expression);

                outMessage = string.Format(_resourceLocator.GetResource(key), propertyName, start != null ? start.ToString() : "null", end != null ? end.ToString() : "null");

                return outMessage;
            }

            return message;
        }

        private bool ShouldResolve(string message)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message)
                && !(message.StartsWith("$") && message.EndsWith("$")))
            {
                return false;
            }

            return true;
        }

        private string PrepareForResolve(string message)
        {
            if ((message.StartsWith("$") && message.EndsWith("$")))
            {
                message = message.Substring(1);
                message = message.Substring(0, message.Length - 1);
            }

            return message;
        }
    }
}
