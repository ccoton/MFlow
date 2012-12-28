﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Validation
{
    public interface IFluentValidationCustomRule<T> 
    {
        IFluentValidation<T> Execute(Func<T> targetFunc);
    }
}