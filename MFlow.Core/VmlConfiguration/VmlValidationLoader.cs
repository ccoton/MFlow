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
                //validator = ParseCustomRules(validator, derivedName);
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
            //validator = ParseLessThan(validator, document);
            //validator = ParseGreaterThan(validator, document);
            //validator = ParseLessThanOrEqualTo(validator, document);
            //validator = ParseGreaterThanOrEqualTo(validator, document);
            //validator = ParseRegEx(validator, document);
            //validator = ParseIsEmail(validator, document);
            //validator = ParseContains(validator, document);
            //validator = ParseEqualExpression(validator, document);
            //validator = ParseNotEqualExpression(validator, document);

            return validator;
        }

        //private IFluentValidation<T> ParseCustomRules<T>(IFluentValidation<T> validator, string fileName)
        //{
        //    var document = LoadDocument(fileName);

        //    var nodes = document.Root.Descendants(XName.Get("Custom")).Descendants();

        //    foreach (var item in nodes)
        //    {
        //        var location = item.Attribute(XName.Get("location")).Value;
        //        var message = item.Attribute(XName.Get("message")).Value;

        //        Assembly.Load(location).GetTypes().Where(t => t.IsClass && typeof(IFluentValidationCustomRule<T>).IsAssignableFrom(t)).ToList()
        //        .ForEach(f =>
        //        {
        //            var customRule = (IFluentValidationCustomRule<T>)Activator.CreateInstance(f);
        //            validator.DependsOn((c) => customRule.Execute(() => validator.GetTarget()), message: message); 
        //        });
        //    }

        //    return validator;
        //}

        private IFluentValidation<T> ParseNotEmpty<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Empty", (e, ev, m, v) => { return validator.NotEmpty(e, m); });
        }

        //private IFluentValidation<T> ParseContains<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, string, string>(validator, document, "Contains", (e, ev, m, v) => { return validator.Contains(e, v, m); });
        //}

        //private IFluentValidation<T> ParseRegEx<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, string, string>(validator, document, "RegEx", (e, ev, m, v) => { return validator.RegEx(e, v, m); });
        //}

        //private IFluentValidation<T> ParseIsEmail<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, string, string>(validator, document, "IsEmail", (e, ev, m, v) => { return validator.IsEmail(e, m); });
        //}

        private IFluentValidation<T> ParseEqual<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "NotEqual", (e, ev, m, v) => { return validator.Equal(e, v, m); });
        }

        //private IFluentValidation<T> ParseEqualExpression<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, string, string>(validator, document, "EqualTo", (e, ev, m, v) => { return validator.Equal(e, ev, m); });
        //}

        private IFluentValidation<T> ParseNotEqual<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "Equal", (e, ev, m, v) => { return validator.NotEqual(e, v, m); });
        }

        //private IFluentValidation<T> ParseNotEqualExpression<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, string, string>(validator, document, "NotEqualTo", (e, ev, m, v) => { return validator.NotEqual(e, ev, m); });
        //}

        //private IFluentValidation<T> ParseLessThan<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, int, int>(validator, document, "LessThan", (e, ev, m, v) => { return validator.LessThan(e, v, m); });
        //}

        //private IFluentValidation<T> ParseGreaterThan<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, int, int>(validator, document, "GreaterThan", (e, ev, m, v) => { return validator.GreaterThan(e, v, m); });
        //}

        //private IFluentValidation<T> ParseLessThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, int, int>(validator, document, "LessThanOrEqualTo", (e, ev, m, v) => { return validator.LessThanOrEqualTo(e, v, m); });
        //}

        //private IFluentValidation<T> ParseGreaterThanOrEqualTo<T>(IFluentValidation<T> validator, XDocument document)
        //{
        //    return CreateExpressions<T, int, int>(validator, document, "GreaterThanOrEqualTo", (e, ev, m, v) => { return validator.GreaterThanOrEqualTo(e, v, m); });
        //}

        private IFluentValidation<T> CreateExpressions<T, O, C>(IFluentValidation<T> validator, string document, string nodeName, Func<Expression<Func<T, O>>, Expression<Func<T, O>>, string, C, IFluentValidation<T>> function)
        {
            var nodes = document.Split(new string[] { Environment.NewLine },StringSplitOptions.None ).ToList();
            nodes = nodes.Where(n => n.ToLower().StartsWith("[display]") && n.ToLower().Contains(string.Format("[is] {0}", nodeName.ToLower()))).ToList();

            foreach (var item in nodes)
            {

                var keys = item.Split(new string[] { "[Display]","[When]","[Is]","[To]" }, StringSplitOptions.None).Where(s=>!string.IsNullOrEmpty(s)).ToList();

                var propertyName = keys.Skip(1).Take(1).Single().Trim();
                var message = keys.Take(1).Single().Trim();
                string valueAttribute = keys.Skip(3).Take(1).SingleOrDefault();
                string toPropertyName = null;

                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, propertyName);

                Expression<Func<T, O>> expression = Expression.Lambda<Func<T, O>>(property, parameter);
                Expression<Func<T, O>> toExpression = null;

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