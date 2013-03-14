using MFlow.Core.Conditions;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///    A validation result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationResult<T>
    {
        /// <summary>
        ///     Exposes the condition that triggered the result
        /// </summary>
        IFluentCondition<T> Condition { get; }
    }
}
