using MFlow.Core.Validation;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MFlow.Loaders.Vml
{
    public class VmlValidationLoader : IFluentValidationLoader
    {
        static IDictionary<string, object> _validators;
        static IFluentValidationFactory _validationFactory;
        object _validatorsLock = new object();

        /// <summary>
        ///     Static constructor
        /// </summary>
        static VmlValidationLoader()
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
            var derivedName = string.IsNullOrEmpty(fileName) ? string.Format("{0}.validation.vml", typeof(T).Name) : fileName;
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
                validator = _validationFactory.GetFluentValidation(target).If(true);
                validator = ParseVml(validator, derivedName);
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

        string LoadDocument(string fileName)
        {
            var path = string.Format(@"{0}\{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            var data = string.Empty;

            using (var stream = File.OpenRead(path))
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

        IFluentValidation<T> ParseVml<T>(IFluentValidation<T> validator, string fileName)
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
            validator = ParseIsNumeric(validator, document);
            validator = ParseIsAlpha(validator, document);
            validator = ParseIsDate(validator, document);
            validator = ParseIsLongerThan(validator, document);
            validator = ParseIsShorterThan(validator, document);
            validator = ParseIsCreditCard(validator, document);
            validator = ParseIsPostCode(validator, document);
            validator = ParseIsZipCode(validator, document);
            validator = ParseIsThisYear(validator, document);
            validator = ParseIsThisMonth(validator, document);
            validator = ParseIsThisWeek(validator, document);
            validator = ParseIsToday(validator, document);
            validator = ParseHasNoItem(validator, document);
            validator = ParseIsNotNull(validator, document);
            validator = ParseIsUsername(validator, document);
            validator = ParseIsPassword(validator, document);

            return validator;
        }

        IFluentValidation<T> ParseCustomRules<T>(IFluentValidation<T> validator, string fileName)
        {
            var document = LoadDocument(fileName);

            if (!document.Contains("\r\n"))
                document = document.Replace("\n", "\r\n");

            var nodes = document.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            nodes = nodes.Where(n => n.ToLower().Contains("[location]") && n.ToLower().StartsWith("[display]", StringComparison.Ordinal)).ToList();

            foreach (var item in nodes)
            {

                var keys = item.Split(new string[]
                {
                    "[Display]",
                    "[When]",
                    "[Location]",
                    "[Hint]"
                }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).ToList();

                var condition = keys.Skip(1).Take(1).Single().Trim();
                var message = keys.Take(1).Single().Trim();
                var location = keys.Skip(2).Take(1).SingleOrDefault();
                var hint = keys.LastOrDefault();

                Assembly.Load(location).GetTypes().Where(t => t.IsClass && t.Name.ToLower() == condition.ToLower() && typeof(IFluentValidationCustomRule<T>).IsAssignableFrom(t)).ToList()
                .ForEach(f =>
                {
                    var customRule = (IFluentValidationCustomRule<T>)Activator.CreateInstance(f);
                    validator.DependsOn((c) => customRule.Execute(() => validator.GetTarget())).Message(message).Hint(hint);
                });
            }

            return validator;
        }

        IFluentValidation<T> ParseHasNoItem<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, ICollection<dynamic>, dynamic>(validator, document, "[HasNoItem] With", (e, ev, m, v, h) =>
            {
                return validator.Check(e).Any(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseNotEmpty<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Empty", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsNotEmpty().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsLength<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotLength ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsLength(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsNumeric<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotNumeric", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsNumeric().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsPassword<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotPassword", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsPassword().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsUsername<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotUsername", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsUsername().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsAlpha<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotAlpha", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsAlpha().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsDate<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotADate", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsDate().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsLongerThan<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotLongerThan ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsLongerThan(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsShorterThan<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, int>(validator, document, "[Is] NotShorterThan ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsShorterThan(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsCreditCard<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotCreditCard", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsCreditCard().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsPostCode<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotPostCode", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsPostCode().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsZipCode<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotZipCode", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsZipCode().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsRequired<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Required", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsRequired<string>().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsNotNull<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Null", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsNotNull<string>().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseContains<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Does] NotContain", (e, ev, m, v, h) =>
            {
                return validator.Check(e).Contains(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseRegEx<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotStringPattern", (e, ev, m, v, h) =>
            {
                return validator.Check(e).Matches(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsEmail<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotAnEmail", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsEmail().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseEqual<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotEqual ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsEqualTo(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseEqualExpression<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] NotEqualToExpression ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsEqualTo(ev).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseNotEqual<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] Equal", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsNotEqualTo(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseNotEqualExpression<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, string, string>(validator, document, "[Is] EqualToExpression ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsNotEqualTo(ev).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseBefore<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] After ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsBefore(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseAfter<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] Before ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsAfter(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsThisYear<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] NotThisYear", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsThisYear().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsThisMonth<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] NotThisMonth", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsThisMonth().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsThisWeek<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] NotThisWeek", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsThisWeek().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseIsToday<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] NotToday", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsToday().Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseOn<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, DateTime, DateTime>(validator, document, "[Is] Not On ", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsOn(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseLessThan<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] GreaterThanOrEqualTo", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsLessThan(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseGreaterThan<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] LessThanOrEqualTo", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsGreaterThan(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseLessThanOrEqualTo<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] GreaterThan", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsLessThanOrEqualTo(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> ParseGreaterThanOrEqualTo<T>(IFluentValidation<T> validator, string document)
        {
            return CreateExpressions<T, int, int>(validator, document, "[Is] LessThan", (e, ev, m, v, h) =>
            {
                return validator.Check(e).IsGreaterThanOrEqualTo(v).Message(m).Hint(h);
            });
        }

        IFluentValidation<T> CreateExpressions<T, O, C>(IFluentValidation<T> validator, string document, string nodeName, Func<Expression<Func<T, O>>, Expression<Func<T, O>>, string, C, string, IFluentValidation<T>> function)
        {
            if (!document.Contains("\r\n"))
                document = document.Replace("\n", "\r\n");

            var nodes = document.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            nodes = nodes.Where(n => !n.ToLower().Contains("[location]") && n.ToLower().StartsWith("[display]", StringComparison.Ordinal) && n.ToLower().Contains(nodeName.ToLower())).ToList();

            foreach (var item in nodes)
            {

                var keys = item.Split(new string[] {
                    "[Display]",
                    "[When]",
                    "[Is]",
                    "[To]",
                    "[Does]",
                    "[Value]",
                    "[RegEx]",
                    "[Exp]",
                    "[Hint]",
                    "[HasNoItem]"
                }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).ToList();

                var propertyName = keys.Skip(1).Take(1).Single().Trim();
                var message = keys.Take(1).Single().Trim();
                var hint = string.Empty;
                if (item.Contains("[Hint]"))
                    hint = keys.LastOrDefault().Trim();
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
                    validator = function(expression, toExpression, message, default(C), hint);
                }
                else
                {
                    var type = default(C);
                    if (type is int)
                    {
                        validator = function(expression, toExpression, message, (C)(object)int.Parse(valueAttribute.Trim()), hint);
                    }
                    else
                        if (type is DateTime)
                        {
                            validator = function(expression, toExpression, message, (C)(object)DateTime.Parse(valueAttribute.Trim()), hint);
                        }
                        else
                        {
                            validator = function(expression, toExpression, message, (C)(object)valueAttribute.Trim(), hint);
                        }
                }

            }

            return validator;
        }
    }
}
