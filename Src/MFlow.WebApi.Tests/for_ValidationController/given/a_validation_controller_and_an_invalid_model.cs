using Machine.Specifications;
using MFlow.WebApi.Api;
using MFlow.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_ValidationController.given
{
    [Subject("for Validation Controller")]
    public class a_validation_controller_and_an_invalid_model
    {
        protected static ValidationController validation_controller;
        protected static ModelToValidate model;

        Establish context = () =>
        {
            validation_controller = new ValidationController();
            model = new ModelToValidate { Validate = JsonConvert.SerializeObject(new User()), Type = "MFlow.WebApi.Tests, MFlow.WebApi.Tests.User" };
        };
    }
}
