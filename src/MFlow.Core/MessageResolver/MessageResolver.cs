﻿using MFlow.Core.Internal;
using MFlow.Core.MessageResolver;
using MFlow.Core.Resources;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.MessageResolver
{
    /// <summary>
    ///     A class for resolving validation messages using expressions
    /// </summary>
    class MessageResolver : IResolveValidationMessages
    {
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IResourceLocator _resourceLocator;

        public MessageResolver ()
        {
            _propertyNameResolver = new PropertyNameResolver ();
            _resourceLocator = new ResourceLocator ();
        }

        /// <summary>
        ///     Resolve a validation message using an property name
        /// </summary>
        public string Resolve (string propertyName, ValidationType type, string message)
        {
            if (ShouldResolve (message)) {
                var customMessage = message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal);

                if (!customMessage && type == ValidationType.Unknown)
                    return string.Empty;

                message = PrepareForResolve (message);
                var key = customMessage ? message : type.ToString ();

                string outMessage = string.Empty;

                outMessage = string.Format (_resourceLocator.GetResource (key), propertyName);

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O> (Expression<Func<T, O>> expression, ValidationType type, string message)
        {
            if (ShouldResolve (message)) {
                var customMessage = message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal);
                message = PrepareForResolve (message);
                var key = customMessage ? message : type.ToString ();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve (expression);

                outMessage = string.Format (_resourceLocator.GetResource (key), propertyName);

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O> (Expression<Func<T, O>> expression, O value, ValidationType type, string message)
        {
            if (ShouldResolve (message)) {
                var customMessage = message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal);
                message = PrepareForResolve (message);
                var key = customMessage ? message : type.ToString ();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve (expression);

                outMessage = string.Format (_resourceLocator.GetResource (key), propertyName, value != null ? value.ToString () : "null");

                return outMessage;
            }

            return message;
        }

        /// <summary>
        ///     Resolve a validation message using an expression
        /// </summary>
        public string Resolve<T, O> (Expression<Func<T, O>> expression, Expression<Func<T, O>> toExpression, ValidationType type, string message)
        {
            if (ShouldResolve (message)) {
                var customMessage = message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal);
                message = PrepareForResolve (message);
                var key = customMessage ? message : type.ToString ();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve (expression);
                var toPropertyName = _propertyNameResolver.Resolve (toExpression);

                outMessage = string.Format(_resourceLocator.GetResource(key), propertyName, toPropertyName);

                return outMessage;
            }

            return message;
        }


        public string Resolve<T, O>(Expression<Func<T, ICollection<O>>> expression, O value, ValidationType type, string message)
        {
            if (ShouldResolve(message))
            {
                var customMessage = message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal);
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
        public string Resolve<T, O> (Expression<Func<T, O>> expression, O start, O end, ValidationType type, string message)
        {
            if (ShouldResolve (message)) {
                var customMessage = message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal);
                message = PrepareForResolve (message);
                var key = customMessage ? message : type.ToString ();

                string outMessage = string.Empty;
                var propertyName = _propertyNameResolver.Resolve (expression);

                outMessage = string.Format (_resourceLocator.GetResource (key), propertyName, start != null ? start.ToString () : "null", end != null ? end.ToString () : "null");

                return outMessage;
            }

            return message;
        }

        bool ShouldResolve (string message)
        {
            if (!string.IsNullOrEmpty (message) && !string.IsNullOrWhiteSpace (message)
                && !(message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal))) {
                return false;
            }

            return true;
        }

        string PrepareForResolve (string message)
        {
            if ((message.StartsWith("$", StringComparison.Ordinal) && message.EndsWith("$", StringComparison.Ordinal))) {
                message = message.Substring (1);
                message = message.Substring (0, message.Length - 1);
            }

            return message;
        }
    }
}
