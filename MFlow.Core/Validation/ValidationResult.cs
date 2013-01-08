using MFlow.Core.Conditions;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A validation result
    /// </summary>
    public class ValidationResult<T> : IValidationResult<T>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ValidationResult(IFluentCondition<T> condition)
        {
            Condition = condition;
        }

        /// <summary>
        ///     Exposes the condtion that triggered the result
        /// </summary>
        public IFluentCondition<T> Condition { get; private set; }
    }
}
