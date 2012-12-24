using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
