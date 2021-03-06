﻿
using MFlow.Core.Internal.Validators.Collections;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation.Validators.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class AnyValidator<T> : IAnyValidator<T>
    {
        public bool Validate(ICollection<T> input, T value)
        {
            if (input == null || input.Count == 0)
                return false;

            return input.First().GetType() == typeof(User);
        }
    }
}
