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
            var type = _types.Where(t => typeof(TValidator).IsAssignableFrom(t) && t.IsClass).FirstOrDefault();
            if (type == null)
                throw new ArgumentException(string.Format("Cannot find a validator for {0}", typeof(TValidator).Name));
            
            var constructor = Expression.Lambda(Expression.New(type.GetConstructor(new Type[]{}))).Compile();
            
            return (TValidator)constructor.DynamicInvoke();
        }
    }
}

