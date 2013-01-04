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
    internal class VmlValidationLoader : IFluentValidationLoader
    {
        private static IDictionary<string, object> _validators;
        private static IDictionary<string, object> _customRules;

        /// <summary>
        ///     Static constructor
        /// </summary>
        static VmlValidationLoader()
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
            var derivedName = string.IsNullOrEmpty(fileName) ? string.Format("{0}.validation.vml", typeof(T).Name) : fileName;
            IFluentValidation<T> validator = null;

            if (_validators.ContainsKey(derivedName))
            {
                validator = (IFluentValidation<T>)_validators.Single(v => v.Key == derivedName).Value;
                validator.SetTarget(target);
            }
            else
            {
                validator = new FluentValidation<T>(target);
                validator = ParseVml(validator, derivedName);
                validator = ParseCustomRules(validator, derivedName);
                _validators.Add(derivedName, validator);
            }

            return validator;
        }

        private string LoadDocument(string fileName)
        {
            var path = string.Format(@"{0}\VmlConfiguration\{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            var data = string.Empty;

            using (var stream = System.IO.File.OpenRead(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                    reader.Close();
                }

                stream.Close();
            }

            return data;
        }

        private IFluentValidation<T> ParseVml<T>(IFluentValidation<T> validator, string fileName)
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
            validator = ParseIsLength(validator, document);
            validator = ParseIsCreditCard(validator, document);
            validator = ParseIsPostCode(validator, document);
            validator = ParseIsZipCode(validator, document);
            validator = ParseIsThisYear(validator, document);

            return validator;
        }

        private IFluentValidation<T> ParseCustomRules<T>(IFluentValidation<T> validator, string fileName)
        {
            var document = LoadDocument(fileName);

            if (!document.Contains("\r\n"))
                document = document.Replace("\n", "\r\n");

            var nodes = document.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            nodes = nodes.Where(n => n.ToLower().Contains("[location]") && n.ToLower().StartsWith("[display]")).ToList();

            foreach (var item in nodes)
            {

                var keys = item.Split(new string[] { "[Display]", "[When]", "[Location]" }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).ToList();

                var condition = keys.Skip(1).Take(1).Single().Trim();
                var message = keys.Take(1).Single().Trim();
                var location = keys.Skip(2).Take(1).SingleOrDefault();

                Assembly.Load(location).GetTypes().Where(t => t.IsClass && t.Name.ToLower() == condition.ToLower() && typeof(IFluentValidationCustomRule<T>).IsAssignableFrom(t)).ToList()
                .ForEach(f =>
                {
                    var customRule = (IFluentValidationCustomRule<T>)Activator.CreateInstance(f);
                    validator.DependsOn((c) => customRule.Execute(() => validator.GetTarget())).Message(message);
                });
            }

            return validator;
        }

        private IFluentValidation<T> ParseNotEmpty<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Empty", (e, ev, m, v) => { return validator.Check(e).IsNotEmpty().Message(m); });
        }

        private IFluentValidation<T> ParseIsLength<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotLength ", (e, ev, m, v) => { return validator.Check(e).IsLength(v).Message(m); });
        }

        private IFluentValidation<T> ParseIsCreditCard<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotCreditCard", (e, ev, m, v) => { return validator.Check(e).IsCreditCard().Message(m); });
        }

        private IFluentValidation<T> ParseIsPostCode<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotPostCode", (e, ev, m, v) => { return validator.Check(e).IsPostCode().Message(m); });
        }

        private IFluentValidation<T> ParseIsZipCode<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotZipCode", (e, ev, m, v) => { return validator.Check(e).IsZipCode().Message(m); });
        }

        private IFluentValidation<T> ParseIsRequired<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Required", (e, ev, m, v) => { return validator.Check(e).IsRequired<string>().Message(m); });
        }

        private IFluentValidation<T> ParseContains<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Does] NotContain", (e, ev, m, v) => { return validator.Check(e).Contains(v).Message(m); });
        }

        private IFluentValidation<T> ParseRegEx<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotStringPattern", (e, ev, m, v) => { return validator.Check(e).Mathes(v).Message(m); });
        }

        private IFluentValidation<T> ParseIsEmail<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotAnEmail", (e, ev, m, v) => { return validator.Check(e).IsEmail().Message(m); });
        }

        private IFluentValidation<T> ParseEqual<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotEqual ", (e, ev, m, v) => { return validator.Check(e).IsEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseEqualExpression<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotEqualToExpression ", (e, ev, m, v) => { return validator.Check(e).IsEqualTo(ev).Message(m); });
        }

        private IFluentValidation<T> ParseNotEqual<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Equal", (e, ev, m, v) => { return validator.Check(e).IsNotEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseNotEqualExpression<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] EqualToExpression ", (e, ev, m, v) => { return validator.Check(e).IsNotEqualTo(ev).Message(m); });
        }

        private IFluentValidation<T> ParseBefore<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] After ", (e, ev, m, v) => { return validator.Check(e).IsBefore(v).Message(m); });
        }

        private IFluentValidation<T> ParseAfter<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] Before ", (e, ev, m, v) => { return validator.Check(e).IsAfter(v).Message(m); });
        }

        private IFluentValidation<T> ParseIsThisYear<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] NotThisYear", (e, ev, m, v) => { return validator.Check(e).IsThisYear().Message(m); });
        }

        private IFluentValidation<T> ParseOn<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] Not On ", (e, ev, m, v) => { return validator.Check(e).IsOn(v).Message(m); });
        }

        private IFluentValidation<T> ParseLessThan<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] GreaterThanOrEqualTo", (e, ev, m, v) => { return validator.Check(e).IsLessThan(v).Message(m); });
        }

        private IFluentValidation<T> ParseGreaterThan<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] LessThanOrEqualTo", (e, ev, m, v) => { return validator.Check(e).IsGreaterThan(v).Message(m); });
        }

        private IFluentValidation<T> ParseLessThanOrEqualTo<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] GreaterThan", (e, ev, m, v) => { return validator.Check(e).IsLessThanOrEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> ParseGreaterThanOrEqualTo<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] LessThan", (e, ev, m, v) => { return validator.Check(e).IsGreaterThanOrEqualTo(v).Message(m); });
        }

        private IFluentValidation<T> CreateExpressions<T, O, C>(IFluentValidation<T> validator, string document, string nodeName, Func<Expression<Func<T, O>>, Expression<Func<T, O>>, string, C, IFluentValidation<T>> function)
        {
            if (!document.Contains("\r\n"))
                document = document.Replace("\n", "\r\n");

            var nodes = document.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            nodes = nodes.Where(n => !n.ToLower().Contains("[location]") && n.ToLower().StartsWith("[display]") && n.ToLower().Contains(nodeName.ToLower())).ToList();

            foreach (var item in nodes)
            {

                var keys = item.Split(new string[] { "[Display]", "[When]", "[Is]", "[To]", "[Does]", "[Value]", "[RegEx]", "[Exp]" }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).ToList();

                var propertyName = keys.Skip(1).Take(1).Single().Trim();
                var message = keys.Take(1).Single().Trim();
                string valueAttribute = keys.Skip(3).Take(1).SingleOrDefault();

                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, propertyName);

                Expression<Func<T, O>> toExpression = null;
                Expression<Func<T, O>> expression = Expression.Lambda<Func<T, O>>(property, parameter);

                string toPropertyName = null;

                if (item.ToLower().Contains("[exp]"))
                {
                    toPropertyName = keys.Last().Trim();
                    valueAttribute = string.Empty;
                }

                if (toPropertyName != null)
                {
                    var toParameter = Expression.Parameter(typeof(T));
                    var toProperty = Expression.Property(toParameter, toPropertyName);
                    toExpression = Expression.Lambda<Func<T, O>>(toProperty, toParameter);
                }

                if (string.IsNullOrEmpty(valueAttribute))
                {
                    validator = function(expression, toExpression, message, default(C));
                }
                else
                {
                    var type = default(C);
                    if (type is int)
                    {
                        validator = function(expression, toExpression, message, (C)(object)int.Parse(valueAttribute.Trim()));
                    }
                    else if (type is DateTime)
                    {
                        validator = function(expression, toExpression, message, (C)(object)DateTime.Parse(valueAttribute.Trim()));
                    }
                    else
                    {
                        validator = function(expression, toExpression, message, (C)(object)valueAttribute.Trim());
                    }
                }

            }

            return validator;
        }
    }
}