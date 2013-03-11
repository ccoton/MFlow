﻿using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_validate : given.a_fluent_user_validator_with_multiple_false_conditions
    {

        static ICollection<IValidationResult<User>> results;

        Because of = () => { results = validator.Validate().ToList(); };

        It should_contain_the_correct_number_of_validation_results = () => { results.Count.ShouldEqual(2); };
        It should_contain_a_first_result_with_the_correct_key = () =>{results.First().Condition.Key.ShouldEqual("Password");};
        It should_contain_a_second_result_with_the_correct_key = () => { results.Skip(1).First().Condition.Key.ShouldEqual("ConfirmPassword"); };
        It should_contain_a_first_result_with_the_correct_message = () => { results.First().Condition.Message.ShouldEqual("Password should be equal to testingx"); };
        It should_contain_a_second_result_with_the_correct_message = () => { results.Skip(1).First().Condition.Message.ShouldEqual("ConfirmPassword should be equal to testing"); };
    }
}
