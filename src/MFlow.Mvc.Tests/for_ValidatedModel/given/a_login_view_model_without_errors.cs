using Machine.Specifications;
using MFlow.Mvc.Tests.Supporting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MFlow.Mvc.Tests.for_ValidatedModel.given
{
    [Subject("for Validated Model")]
    public class a_login_view_model_without_errors
    {
        protected static LoginViewModel model;
        protected static Exception exception = null;
        protected static ICollection<ValidationResult> results;

        Establish context = () =>
        {
            model = new LoginViewModel()
            {
                Username = "testing",
                Password = "password"
            };
        };
    }
}
