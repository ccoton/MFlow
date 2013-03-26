using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationCollection<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to a collection containing any item equal to the value
        /// </summary>
        public IFluentValidation<T> Any<C>(C value)
        {
            return ApplyGenericCollectionValidator(_validatorFactory.GetValidators<ICollection<C>, C, IAnyValidator<C>>(), Enums.ValidationType.Any, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a collection not containing any item equal to the value
        /// </summary>
        public IFluentValidation<T> None<C>(C value)
        {
            return ApplyGenericCollectionValidator(_validatorFactory.GetValidators<ICollection<C>, C, INoneValidator<C>>(), Enums.ValidationType.None, value);
        }

        public IFluentValidation<T> All<C>(ICollection<C> value)
        {
            return ApplyGenericCollectionValidator(_validatorFactory.GetValidators<ICollection<C>, ICollection<C>, IAllValidator<C>>(), Enums.ValidationType.All, value);
        }

        IFluentValidation<T> ApplyGenericCollectionValidator<C>(ICollection<IComparisonValidator<ICollection<C>, C>> validators, Enums.ValidationType type, C value)
        {
            _validatorToCondition.ForCollectionOf<C>(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        IFluentValidation<T> ApplyGenericCollectionValidator<C>(ICollection<IComparisonValidator<ICollection<C>, ICollection<C>>> validators, Enums.ValidationType type, ICollection<C> values)
        {
            _validatorToCondition.ForCollectionOf<C>(_currentContext, validators, type, values)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }
    }
}
