﻿namespace MFlow.Core.Internal.Validators
{
    /// <summary>
    ///     A validator
    /// </summary>
    public interface IValidator<TInput> : ICanValidate
    {
        /// <summary>
        ///     A validate method
        /// </summary>
        bool Validate (TInput input);
    }
}
