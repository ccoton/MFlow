using Machine.Specifications;
using MFlow.WebApi.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_ValidationController.given
{
    [Subject("for Validation Controller")]
    public class a_validation_controller
    {
        protected static ValidationController validation_controller;

        Establish context = () =>
        {
            validation_controller = new ValidationController();
        };
    }
}
