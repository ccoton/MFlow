﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Factories;

namespace MFlow.Loaders.Xml
{
    /// <summary>
    ///     Load a fluentvalidation configuration
    /// </summary>
    public class XmlValidationLoader : IFluentValidationLoader
    {
        static IDictionary<string, object> _validators;
        static IFluentValidationFactory _validationFactory;

        object _validatorsLock = new object();


        /// <summary>
        ///     Static type initializer
        /// </summary>
        static XmlValidationLoader()
        {
            _validators = new Dictionary<string, object>();
            _validationFactory = new FluentValidationFactory();
        }

        /// <summary>
        ///     Load the configuration
        /// </summary>
        public IFluentValidation<T> Load<T>(T target, string fileName = "") where T : class
        {
            var validator = LoadAndCache(target, fileName);

            return validator;
        }

        IFluentValidation<T> LoadAndCache<T>(T target, string fileName = "") where T : class
        {
            var derivedName = string.IsNullOrEmpty(fileName) ? string.Format("{0}.validation.xml", typeof(T).Name) : fileName;
            IFluentValidation<T> validator = null;

            if (_validators.ContainsKey(derivedName))
            {
                validator = (IFluentValidation<T>)_validators.Single(v => v.Key == derivedName).Value;
                validator.SetTarget(target);
            }
            else
            {
                // Call if(true) just to return the actual Validator
                // instead of the builder
                validator = _validationFactory.CreateFor(target).If(true);
                validator = ParseXml(validator, derivedName);
                validator = ParseCustomRules(validator, derivedName);

                lock (_validatorsLock)
                {
                    if (!_validators.ContainsKey(derivedName))
                    {
                        _validators.Add(derivedName, validator);
                    }
                }
            }

            return validator;
        }

        XDocument LoadDocument(string fileName)
        {
            var path = string.Format(@"{0}\{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            var document = XDocument.Load(path);
            return document;
        }

        IFluentValidation<T> ParseXml<T>(IFluentValidation<T> validator, string fileName)
        {
            var document = LoadDocument(fileName);

            validator = ParseNotEmpty(validator, document);
            validator = ParseEqual(validator, document);
            validator = ParseNotEqual(validator, document);
            validator = ParseLessThan(validator, document);
            validator = ParseGreaterThan(validator, document);
            validator = ParseLessThanOrEqualTo(validator, document);
            validator = ParseGreaterThanOrEqualTo(validator, document);
            validator = ParseRegEx(validator, document);
            validator = ParseIsEmail(validator, document);
            validator = ParseIsDate(validator, document);
            validator = ParseContains(validator, document);
            validator = ParseEqualExpression(validator, document);
            validator = ParseNotEqualExpression(validator, document);
            validator = ParseAfter(validator, document);
            validator = ParseBefore(validator, document);
            validator = ParseOn(validator, document);
            validator = ParseIsRequired(validator, document);
            validator = ParseIsLength(validator, document);
            validator = ParseIsNumeric(validator, document);
            validator = ParseIsAlpha(validator, document);
            validator = ParseIsLongerThan(validator, document);
            validator = ParseIsShorterThan(validator, document);
            validator = ParseIsCreditCard(validator, document);
            validator = ParseIsPostCode(validator, document);
            validator = ParseIsZipCode(validator, document);
            validator = ParseIsThisYear(validator, document);
            validator = ParseIsThisMonth(validator, document);
            validator = ParseIsThisWeek(validator, document);
            validator = ParseIsToday(validator, document);
            validator = ParseIsNotNull(validator, document);
            validator = ParseIsPassword(validator, document);
            validator = ParseIsUsername(validator, document);
            validator = ParseIsUrl(validator, document);

            return validator;
        }

        IFluentValidation<T> ParseCustomRules<T>(IFluentValidation<T> validator, string fileName)
        {
            var document = LoadDocument(fileName);

            var nodes = document.Root.Descendants(XName.Get("Custom")).Descendants();

            foreach (var item in nodes)
            {
                var condition = item.Name.ToString();
                var location = item.Attribute(XName.Get("location")).Value;

                var message = string.Empty;

                var messageAttribute = item.Attribute(XName.Get("message"));
                var hintAttribute = item.Attribute(XName.Get("hint"));

                if (messageAttribute != null)
                    message = messageAttribute.Value;

                if (hintAttribute != null) {
					var hint = string.Empty;
					hint = hintAttribute.Value;
				}

                Assembly.Load(location).GetTypes().Where(t => t.IsClass && t.Name.ToLower() == condition.ToLower() && typeof(IFluentValidationCustomRule<T>).IsAssignableFrom(t)).ToList()
                .ForEach(f =>
                {
                    var customRule = (IFluentValidationCustomRule<T>)Activator.CreateInstance(f);
                    validator.DependsOn((c) => customRule.Execute(() => validator.GetTarget())).Message(message); 
                });
            }

            return validator;
        }

        IFluentValidation<T> ParseNotEmpty<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEmpty", (e, ev, m, v, h) => {
                return validator.Check(e).IsNotEmpty().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsRequired<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsRequired", (e, ev, m, v, h) => {
                return validator.Check(e).IsRequired<string>().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsNotNull<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsNotNull", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsNotNull<string>().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsPassword<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsPassword", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsPassword().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsUsername<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsUsername", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsUsername().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsUrl<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsUrl", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsUrl().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsLength<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsLength", (e, ev, m, v, h) => {
                return validator.Check(e).IsLength(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsNumeric<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsNumeric", (e, ev, m, v, h) => {
                return validator.Check(e).IsNumeric().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsAlpha<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsAlpha", (e, ev, m, v, h) => {
                return validator.Check(e).IsAlpha().Message(m).Hint(h); });
        }
        
        IFluentValidation<T> ParseIsDate<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsDate", (e, ev, m, v, h) => {
                return validator.Check(e).IsDate().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsLongerThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsLongerThan", (e, ev, m, v, h) => {
                return validator.Check(e).IsLongerThan(v).Message(m).Hint(h); });
        }
        
        IFluentValidation<T> ParseIsShorterThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsShorterThan", (e, ev, m, v, h) => {
                return validator.Check(e).IsShorterThan(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsCreditCard<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsCreditCard", (e, ev, m, v, h) => {
                return validator.Check(e).IsCreditCard().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsPostCode<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsPostCode", (e, ev, m, v, h) => {
                return validator.Check(e).IsPostCode().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsZipCode<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, int>(validator, document, "IsZipCode", (e, ev, m, v, h) => {
                return validator.Check(e).IsZipCode().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseContains<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Contains", (e, ev, m, v, h) => {
                return validator.Check(e).Contains(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseRegEx<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "RegEx", (e, ev, m, v, h) => {
                return validator.Check(e).Matches(v).Message(m).Hint(h); });
        }
    
        IFluentValidation<T> ParseIsEmail<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsEmail", (e, ev, m, v, h) => {
                return validator.Check(e).IsEmail().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseEqual<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Equal", (e, ev, m, v, h) => {
                return validator.Check(e).IsEqualTo(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseEqualExpression<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "EqualTo", (e, ev, m, v, h) => {
                return validator.Check(e).IsEqualTo(ev).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseNotEqual<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEqual", (e, ev, m, v, h) => {
                return validator.Check(e).IsNotEqualTo(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseNotEqualExpression<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEqualTo", (e, ev, m, v, h) => {
                return validator.Check(e).IsNotEqualTo(ev).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseLessThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "LessThan", (e, ev, m, v, h) => {
                return validator.Check(e).IsLessThan(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseGreaterThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "GreaterThan", (e, ev, m, v, h) => {
                return validator.Check(e).IsGreaterThan(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseLessThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "LessThanOrEqualTo", (e, ev, m, v, h) => {
                return validator.Check(e).IsLessThanOrEqualTo(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseGreaterThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "GreaterThanOrEqualTo", (e, ev, m, v, h) => {
                return validator.Check(e).IsGreaterThanOrEqualTo(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseAfter<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "After", (e, ev, m, v, h) => {
                return validator.Check(e).IsAfter(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseBefore<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "Before", (e, ev, m, v, h) => {
                return validator.Check(e).IsBefore(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseOn<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "On", (e, ev, m, v, h) => {
                return validator.Check(e).IsOn(v).Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsThisYear<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "IsThisYear", (e, ev, m, v, h) => {
                return validator.Check(e).IsThisYear().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsThisMonth<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "IsThisMonth", (e, ev, m, v, h) => {
                return validator.Check(e).IsThisMonth().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsThisWeek<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "IsThisWeek", (e, ev, m, v, h) => {
                return validator.Check(e).IsThisWeek().Message(m).Hint(h); });
        }

        IFluentValidation<T> ParseIsToday<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "IsToday", (e, ev, m, v, h) => {
                return validator.Check(e).IsToday().Message(m).Hint(h); });
        }

        IFluentValidation<T> CreateExpressions<T, O, C>(IFluentValidation<T> validator, XDocument document, string nodeName, Func<Expression<Func<T, O>>, Expression<Func<T, O>>, string, C, string, IFluentValidation<T>> function)
        {
            var nodes = document.Root.Descendants(XName.Get(nodeName));

            foreach (var item in nodes)
            {
                var propertyName = item.Attribute(XName.Get("property")).Value;
                var messageAttribute = item.Attribute(XName.Get("message"));
                var hintAttribute = item.Attribute(XName.Get("hint"));
                var valueAttribute = item.Attribute(XName.Get("value"));
                var toPropertyName = item.Attribute(XName.Get("toProperty"));

                var message = string.Empty;
                var hint = string.Empty;

                if (messageAttribute != null)
                    message = messageAttribute.Value;

                if (hintAttribute != null)
                    hint = hintAttribute.Value;

                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, propertyName);

                Expression<Func<T, O>> expression = Expression.Lambda<Func<T, O>>(property, parameter);
                Expression<Func<T, O>> toExpression = null;

                if (toPropertyName != null)
                {
                    var toParameter = Expression.Parameter(typeof(T));
                    var toProperty = Expression.Property(toParameter, toPropertyName.Value);
                    toExpression = Expression.Lambda<Func<T, O>>(toProperty, toParameter);
                }

                if (valueAttribute == null)
                {
                    validator = function(expression, toExpression, message, default(C), hint);
                }
                else
                {
                    var type = default(C);
                    if (type is int)
                    {
                        validator = function(expression, toExpression, message, (C)(object)int.Parse(valueAttribute.Value), hint);
                    }
                    else
                    if (type is DateTime)
                    {
                        validator = function(expression, toExpression, message, (C)(object)DateTime.Parse(valueAttribute.Value), hint);
                    }
                    else
                    {
                        validator = function(expression, toExpression, message, (C)(object)valueAttribute.Value, hint);
                    }
                }

            }

            return validator;
        }
    }
}