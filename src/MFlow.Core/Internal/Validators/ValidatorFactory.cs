using System;
using MFlow.Core.Internal.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MFlow.Core
{
    class ValidatorFactory : IValidatorFactory
    {
        readonly static ICollection<Assembly> _assemblies = AppDomain.CurrentDomain.GetAssemblies();
        readonly static ICollection<Type> _types = _assemblies.SelectMany(a => a.GetTypes()).ToList();

        /// <summary>
        ///     Gets the validator.
        /// </summary>
        public ICollection<IValidator<T>> GetValidator<T, ValidatorT>() where ValidatorT : IValidator<T>
        {
            var items = new List<ValidatorT>();
            items.AddRange(GetValidator<ValidatorT>());
            return items.Cast<IValidator<T>>().ToList();
        }

        /// <summary>
        ///     Gets the validator.
        /// </summary>
        public ICollection<IComparisonValidator<TInput, TCompare>> GetValidator<TInput, TCompare, ValidatorT>() where ValidatorT : IComparisonValidator<TInput, TCompare>
        {
            var items = new List<ValidatorT>();
            items.AddRange(GetValidator<ValidatorT>());
            return items.Cast<IComparisonValidator<TInput, TCompare>>().ToList();
        }

        ICollection<TValidator> GetValidator<TValidator>()
        {
            var items = new List<TValidator>();
            Type type = null;
            if (!typeof(TValidator).IsGenericType)
            {
                _types.Where(t => typeof(TValidator).IsAssignableFrom(t) && t.IsClass).ToList()
                    .ForEach(i =>
                    {
                        if (!i.IsGenericType)
                        {
                            var constructor = Expression.Lambda(Expression.New(i.GetConstructor(Type.EmptyTypes))).Compile();
                            items.Add((TValidator)constructor.DynamicInvoke());
                        }
                    });
            }
            else
            {
                foreach (var theType in _types)
                {
                    foreach (var item in theType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(TValidator).GetGenericTypeDefinition())
                            .Select(t => t.GetGenericArguments().First())
                            .ToList())
                    {

                        if (item != null)
                        {
                            type = item.DeclaringType;
                            type = type.MakeGenericType(typeof(TValidator).GetGenericArguments());
                            if (type.IsGenericType)
                            {
                                items.Add((TValidator)(type.GetConstructor(Type.EmptyTypes)).Invoke(new object[] { }));
                            }
                        }
                    }
                }
            }

            return items;
        }
    }
}

