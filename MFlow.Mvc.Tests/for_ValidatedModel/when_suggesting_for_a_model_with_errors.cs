using Machine.Specifications;
using MFlow.Mvc.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MFlow.Mvc.Tests.for_ValidatedModel
{
    public class when_suggesting_for_a_model_with_errors : given.a_login_view_model_with_errors
    {
        Because of = () => { exception = Catch.Exception(() => { results = model.Suggest(new System.ComponentModel.DataAnnotations.ValidationContext(model, null, null)).ToList(); }); };
        It should_not_throw_an_argument_null_exception = () => { exception.ShouldBeNull(); };
        It should_have_two_suggestions = () => { results.Count.ShouldEqual(2); };
    }
}
