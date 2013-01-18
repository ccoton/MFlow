﻿using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Longer Validator
    /// </summary>
    class LongerValidator : ILongerValidator
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool>();

        public bool Validate(string input, int value)
        {
            if (input == null)
                return false;

            var key = string.Format("{0} == {1}", input, value);

            if (!cache.ContainsKey(key))
                cache [key] = input.Length > value;
            return cache [key];
        }
    }
}
