using System;
using MFlow.Core.Internal.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MFlow.Core
{
    class ValidatorFactory : IValidatorFactory
    {
        readonly static IList<Type> _types;

        static ValidatorFactory()
        {
            _types = typeof(ValidatorFactory).Assembly.GetTypes().ToList();
        }
      
        /// <summary>
        ///     Gets the validator.
        /// </summary>
        public IValidator<T> GetValidator<T, ValidatorT>() where ValidatorT : IValidator<T>
        {
            return GetValidator<ValidatorT>();
        }

        /// <summary>
        ///     Gets the validator.
        /// </summary>
        public IComparisonValidator<TInput, TCompare> GetValidator<TInput, TCompare, ValidatorT>() where ValidatorT : IComparisonValidator<TInput, TCompare>
        {
            return GetValidator<ValidatorT>();
        }

        TValidator GetValidator<TValidator>()
        {
            Type type = null;
            if (!typeof(TValidator).IsGenericType)
            {
                type = _types.FirstOrDefault(t => typeof(TValidator).IsAssignableFrom(t) && t.IsClass);
            }
            else
            {
                foreach (var theType in _types)
                {
                    var item = theType.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(TValidator).GetGenericTypeDefinition())
                            .Select(t => t.GetGenericArguments().First())
                            .FirstOrDefault();

                    if (item != null)
                    {
                        type = item.DeclaringType;
                        type = type.MakeGenericType(typeof(TValidator).GetGenericArguments());
                        break;
                    }
                }
            }

            if (type == null)
                throw new ArgumentException(string.Format("Cannot find a validator for {0}", typeof(TValidator).Name));

            if (type.IsGenericType)
            {
                return (TValidator)(type.GetConstructor(Type.EmptyTypes)).Invoke(new object[] { });
            }

            var constructor = Expression.Lambda(Expression.New(type.GetConstructor(Type.EmptyTypes))).Compile();
            
            return (TValidator)constructor.DynamicInvoke();
        }
    }
}

