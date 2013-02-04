using MFlow.Core.Conditions;
using System;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A validation result
    /// </summary>
    public class ValidationResult<T> : IValidationResult<T>
    {
        private ValidationResult() { }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ValidationResult(IFluentCondition<T> condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");
            Condition = condition;
        }

        /// <summary>
        ///     Exposes the condtion that triggered the result
        /// </summary>
        public IFluentCondition<T> Condition { get; private set; }
    }
}
