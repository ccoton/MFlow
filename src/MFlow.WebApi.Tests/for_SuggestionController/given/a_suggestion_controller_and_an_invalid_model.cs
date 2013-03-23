using Machine.Specifications;
using MFlow.WebApi.Api;
using MFlow.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_SuggestionController.given
{
    [Subject("for Suggestion Controller")]
    public class a_suggestion_controller_and_an_invalid_model
    {
        protected static SuggestionController suggestion_controller;
        protected static ModelToValidate model;

        Establish context = () =>
        {
            suggestion_controller = new SuggestionController();
            model = new ModelToValidate { Validate = JsonConvert.SerializeObject(new User()), Type = "MFlow.WebApi.Tests, MFlow.WebApi.Tests.User" };
        };
    }
}
