﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using MFlow.Core.Validation;

namespace MFlow.Mvc
{
    /// <summary>
    ///     An base class that an MVC view model can inherit from
    /// </summary>
    public class ValidatedModel<T> : IValidatedModel<T>, IValidatableObject, ISuggestableObject where T : class
    {
        IFluentValidationBuilder<T> _validator;
        IFluentValidationFactory _factory;

        public ValidatedModel()
        {
            _factory = new FluentValidationFactory();
        }

        /// <summary>
        ///     The validation instance
        /// </summary>
        public IFluentValidationBuilder<T> GetValidator(T target, string rulesetFile = "", Func<IFluentValidationLoader> loader = null)
        {
            if (_validator == null)
                if (loader == null)
                    _validator = _factory.CreateFor<T>(target);
                else
                    if (loader != null)
                        _validator = (IFluentValidationBuilder<T>)loader().Load<T>(target, rulesetFile);
            return _validator;
        }

        /// <summary>
        ///     Validates the current object instance against the validator
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _validator.SetTarget((T)validationContext.ObjectInstance);
            var validate = _validator.Validate().ToList();
            return validate.Select(v => new ValidationResult(v.Condition.Message, new[] { v.Condition.Key }));
        }

        /// <summary>
        ///      Validates the current object instance against the validator, providing pre validation suggestions
        /// </summary>
        public IEnumerable<ValidationResult> Suggest(ValidationContext validationContext)
        {
            _validator.SetTarget((T)validationContext.ObjectInstance);
            var validate = _validator.Validate().ToList();
            return validate.Where(v => !string.IsNullOrEmpty(v.Condition.Hint)).Select(v => new ValidationResult(v.Condition.Hint, new[] { v.Condition.Key }));
        }

    }
}
