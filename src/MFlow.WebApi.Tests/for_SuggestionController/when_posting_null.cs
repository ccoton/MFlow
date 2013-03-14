using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_SuggestionController
{
    public class when_posting_null : given.a_suggestion_controller
    {
        static Exception exception = null;

        Because of = () => { exception = Catch.Exception(() => { suggestion_controller.Post(null); }); };

        It should_throw_an_argument_exception = () => { exception.ShouldBeOfType<ArgumentException>(); };
    }
}
