﻿using MFlow.Core.Validation.Validators.Strings;
using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Shorter Validator
    /// </summary>
    class ShorterValidator : IShorterValidator
    {
        public bool Validate(string input, int value)
        {
            if (input == null)
                return false;

            return input.Length < value;
        }
    }
}
