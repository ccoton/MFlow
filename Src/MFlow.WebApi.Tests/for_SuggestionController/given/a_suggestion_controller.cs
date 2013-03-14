using Machine.Specifications;
using MFlow.WebApi.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.WebApi.Tests.for_SuggestionController.given
{
    [Subject("for Suggestion Controller")]
    public class a_suggestion_controller
    {
        protected static SuggestionController suggestion_controller;

        Establish context = () =>
        {
            suggestion_controller = new SuggestionController();
        };
    }
}
