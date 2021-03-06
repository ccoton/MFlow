﻿using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_throw_with_false_condition : given.a_fluent_user_validator_with_false_condition
    {
        static Exception exception = null;

        Because of = () => { exception = Catch.Exception(() => { ((IFluentValidation<User>)validator).Throw(new Exception()); }); };

        It should_not_throw_an_exception = () => { exception.ShouldBeNull(); };
    }
}
