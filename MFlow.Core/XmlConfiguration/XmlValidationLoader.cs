using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MFlow.Core.Validation;

namespace MFlow.Core.XmlConfiguration
{
    /// <summary>
    ///     Load a fluentvalidation configuration
    /// </summary>
    internal class XmlValidationLoader : IFluentValidationLoader
    {
        private static IDictionary<string, object> _validators;
        private static IDictionary<string, object> _customRules;

        /// <summary>
        ///     Static constructor
        /// </summary>
        static XmlValidationLoader()
        {
            _validators = new Dictionary<string, object>();
            _customRules = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Load the configuration
        /// </summary>
        public IFluentValidation<T> Load<T>(T target, string fileName = "")
        {
            var validator = LoadAndCache(target, fileName);

            return validator;
        }

        private IFluentValidation<T> LoadAndCache<T>(T target, string fileName = "")
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
                validator = new FluentValidation<T>(target);
                validator = ParseXml(validator, derivedName);
                validator = ParseCustomRules(validator, derivedName);
                _validators.Add(derivedName, validator);
            }

            return validator;
        }

        private XDocument LoadDocument(string fileName)
        {
            var path = string.Format(@"{0}\XmlConfiguration\{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            var document = XDocument.Load(path);
            return document;
        }

        private IFluentValidation<T> ParseXml<T>(IFluentValidation<T> validator, string fileName)
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
            validator = ParseContains(validator, document);
            validator = ParseEqualExpression(validator, document);
            validator = ParseNotEqualExpression(validator, document);
            validator = ParseAfter(validator, document);
            validator = ParseBefore(validator, document);
            validator = ParseOn(validator, document);
            validator = ParseIsRequired(validator, document);

            return validator;
        }

        private IFluentValidation<T> ParseCustomRules<T>(IFluentValidation<T> validator, string fileName)
        {
            var document = LoadDocument(fileName);

            var nodes = document.Root.Descendants(XName.Get("Custom")).Descendants();

            foreach (var item in nodes)
            {
                var condition = item.Name.ToString();
                var location = item.Attribute(XName.Get("location")).Value;

                var message = string.Empty;
                var messageAttribute = item.Attribute(XName.Get("message"));

                if(messageAttribute!= null)
                    message = messageAttribute.Value;

                Assembly.Load(location).GetTypes().Where(t => t.IsClass && t.Name.ToLower() == condition.ToLower() && typeof(IFluentValidationCustomRule<T>).IsAssignableFrom(t)).ToList()
                .ForEach(f =>
                {
                    var customRule = (IFluentValidationCustomRule<T>)Activator.CreateInstance(f);
                    validator.DependsOn((c) => customRule.Execute(() => validator.GetTarget())).Message(message); 
                });
            }

            return validator;
        }

        private IFluentValidation<T> ParseNotEmpty<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEmpty", (e, ev, m, v) => { return validator.Check(e).IsNotEmpty().Message(m); });
        }

        private IFluentValidation<T> ParseIsRequired<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsRequired", (e, ev, m, v) => { return validator.Check(e).IsRequired<string>().Message(m); });
        }

        private IFluentValidation<T> ParseContains<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Contains", (e, ev, m, v) => { return validator.Check(e).Contains(v).Message(m); });
        }

        private IFluentValidation<T> ParseRegEx<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "RegEx", (e, ev, m, v) => { return validator.Check(e).Mathes(v).Message(m); });
        }

        private IFluentValidation<T> ParseIsEmail<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsEmail", (e, ev, m, v) => { return validator.Check(e).IsEmail().Message(m); });
        }

        private IFluentValidation<T> ParseEqual<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Equal", (e, ev, m, v) => { return validator.Check(e).IsEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseEqualExpression<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "EqualTo", (e, ev, m, v) => { return validator.Check(e).IsEqualTo(ev).Message(m); });
        }

        private IFluentValidation<T> ParseNotEqual<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEqual", (e, ev, m, v) => { return validator.Check(e).IsNotEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseNotEqualExpression<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEqualTo", (e, ev, m, v) => { return validator.Check(e).IsNotEqualTo(ev).Message(m); });
        }

        private IFluentValidation<T> ParseLessThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "LessThan", (e, ev, m, v) => { return validator.Check(e).IsLessThan(v).Message(m); });
        }

        private IFluentValidation<T> ParseGreaterThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "GreaterThan", (e, ev, m, v) => { return validator.Check(e).IsGreaterThan(v).Message(m); });
        }

        private IFluentValidation<T> ParseLessThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "LessThanOrEqualTo", (e, ev, m, v) => { return validator.Check(e).IsLessThanOrEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseGreaterThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "GreaterThanOrEqualTo", (e, ev, m, v) => { return validator.Check(e).IsGreaterThanOrEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseAfter<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "After", (e, ev, m, v) => { return validator.Check(e).After(v).Message(m); });
        }

        private IFluentValidation<T> ParseBefore<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "Before", (e, ev, m, v) => { return validator.Check(e).Before(v).Message(m); });
        }

        private IFluentValidation<T> ParseOn<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "On", (e, ev, m, v) => { return validator.Check(e).On(v).Message(m); });
        }

        private IFluentValidation<T> CreateExpressions<T, O, C>(IFluentValidation<T> validator, XDocument document, string nodeName, Func<Expression<Func<T, O>>, Expression<Func<T, O>>, string, C, IFluentValidation<T>> function)
        {
            var nodes = document.Root.Descendants(XName.Get(nodeName));

            foreach (var item in nodes)
            {
                var propertyName = item.Attribute(XName.Get("property")).Value;
                var messageAttribute = item.Attribute(XName.Get("message"));
                var valueAttribute = item.Attribute(XName.Get("value"));
                var toPropertyName = item.Attribute(XName.Get("toProperty"));

                var message = string.Empty;

                if (messageAttribute != null)
                    message = messageAttribute.Value;

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
                    validator = function(expression, toExpression, message, default(C));
                }
                else
                {
                    var type = default(C);
                    if (type is int)
                    {
                        validator = function(expression, toExpression, message, (C)(object)int.Parse(valueAttribute.Value));
                    }
                    else if (type is DateTime)
                    {
                        validator = function(expression, toExpression, message, (C)(object)DateTime.Parse(valueAttribute.Value));
                    }
                    else
                    {
                        validator = function(expression, toExpression, message, (C)(object)valueAttribute.Value.ToString());
                    }
                }

            }

            return validator;
        }
    }
}