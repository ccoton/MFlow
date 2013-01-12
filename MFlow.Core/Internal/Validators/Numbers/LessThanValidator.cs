﻿namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     LessThan Validator
    /// </summary>
    class LessThanValidator : ICompareValidator<int, int>
    {
        public bool Validate (int input, int value)
        {
            return input < value;
        }
    }
}
