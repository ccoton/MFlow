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
    internal class FluentValidationLoader : IFluentValidationLoader
    {
        private static IDictionary<string, object> _validators;
        private static IDictionary<string, object> _customRules;

        /// <summary>
        ///     Static constructor
        /// </summary>
        static FluentValidationLoader()
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
                _validators.Add(derivedName, validator);
            }

            return validator;
        }

        private IFluentValidation<T> ParseXml<T>(IFluentValidation<T> validator, string fileName)
        {
            var path = string.Format(@"{0}\XmlConfiguration\{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            var document = XDocument.Load(path);

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
            validator = ParseCustomRules(validator, document);

            return validator;
        }

        private IFluentValidation<T> ParseCustomRules<T>(IFluentValidation<T> validator, XDocument document)
        {

            var nodes = document.Root.Descendants(XName.Get(string.Format("{0}CustomRule", typeof(T).Name)));

            foreach (var item in nodes)
            {
                var location = item.Attribute(XName.Get("location")).Value;
                Assembly.Load(location).GetTypes().Where(t => t.IsClass && typeof(IFluentValidationCustomRule<T>).IsAssignableFrom(t)).ToList()
                .ForEach(f =>
                {
                    var customRule = (IFluentValidationCustomRule<T>)Activator.CreateInstance(f);
                    validator = customRule.Execute(validator);
                });
            }

            return validator;
        }

        private IFluentValidation<T> ParseNotEmpty<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEmpty", (e, m, v) => { return validator.NotEmpty(e, m); });
        }

        private IFluentValidation<T> ParseContains<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Contains", (e, m, v) => { return validator.Contains(e, v, m); });
        }

        private IFluentValidation<T> ParseRegEx<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "RegEx", (e, m, v) => { return validator.RegEx(e, v, m); });
        }

        private IFluentValidation<T> ParseIsEmail<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "IsEmail", (e, m, v) => { return validator.IsEmail(e, m); });
        }

        private IFluentValidation<T> ParseEqual<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Equal", (e, m, v) => { return validator.Equal(e, v, m); });
        }

        private IFluentValidation<T> ParseNotEqual<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEqual", (e, m, v) => { return validator.NotEqual(e, v, m); });
        }

        private IFluentValidation<T> ParseLessThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "LessThan", (e, m, v) => { return validator.LessThan(e, v, m); });
        }

        private IFluentValidation<T> ParseGreaterThan<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "GreaterThan", (e, m, v) => { return validator.GreaterThan(e, v, m); });
        }

        private IFluentValidation<T> ParseLessThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "LessThanOrEqualTo", (e, m, v) => { return validator.LessThanOrEqualTo(e, v, m); });
        }

        private IFluentValidation<T> ParseGreaterThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        {
            return CreateExpressions<T, int, int>(validator, document, "GreaterThanOrEqualTo", (e, m, v) => { return validator.GreaterThanOrEqualTo(e, v, m); });
        }

        private IFluentValidation<T> CreateExpressions<T, O, C>(IFluentValidation<T> validator, XDocument document, string nodeName, Func<Expression<Func<T, O>>, string, C, IFluentValidation<T>> function)
        {
            var nodes = document.Root.Descendants(XName.Get(nodeName));

            foreach (var item in nodes)
            {
                var propertyName = item.Attribute(XName.Get("property")).Value;
                var message = item.Attribute(XName.Get("message")).Value;
                var valueAttribute = item.Attribute(XName.Get("value"));

                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, propertyName);

                Expression<Func<T, O>> expression = Expression.Lambda<Func<T, O>>(property, parameter);

                if (valueAttribute == null)
                {
                    validator = function(expression, message, default(C));
                }
                else
                {
                    var type = default(C);
                    if (type is int)
                    {
                        validator = function(expression, message, (C)(object)int.Parse(valueAttribute.Value));
                    }
                    else
                    {
                        validator = function(expression, message, (C)(object)valueAttribute.Value.ToString());
                    }
                }

            }

            return validator;
        }
    }
}