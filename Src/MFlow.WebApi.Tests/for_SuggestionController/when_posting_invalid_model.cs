using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_SuggestionController
{
    public class when_posting_invalid_model : given.a_suggestion_controller_and_an_invalid_model
    {
        static Exception exception = null;
        static ICollection<ValidationResult> results;

        Because of = () => { exception = Catch.Exception(() => { results = suggestion_controller.Post(model).ToList(); }); };

        It should_not_throw_an_argument_exception = () => { exception.ShouldBeNull(); };
        It should_return_the_correct_number_of_suggestions = () => { results.Count.ShouldEqual(2); };
        It should_return_the_correct_message_for_the_first_result = () => { results.First().ErrorMessage.ShouldEqual("Enter a username"); };
        It should_return_the_correct_message_for_the_second_result = () => { results.Skip(1).First().ErrorMessage.ShouldEqual("Enter a password"); };
    }
}
