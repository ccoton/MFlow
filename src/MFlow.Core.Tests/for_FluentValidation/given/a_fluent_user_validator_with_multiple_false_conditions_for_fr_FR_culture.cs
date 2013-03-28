using Machine.Specifications;
using MEvents.Core;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace MFlow.Core.Tests.for_FluentValidation.given
{
    [Subject("for Fluent Validation")]
    public class a_fluent_user_validator_with_multiple_false_conditions_for_fr_FR_culture
    {
        protected static IFluentValidationBuilder<User> validator;
        protected static User user;
        protected static IEventCoordinator event_coordinator = new EventsFactory().GetEventCoordinator();
        protected static CultureInfo default_culture;
        Establish context = () =>
        {
            default_culture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            user = new User();
            user.Manager = new User();
            validator = new FluentValidationFactory().CreateFor<User>(user);
            user.Password = "testing";
            validator
                .Check(u => u.Password).IsEqualTo("testingx")
                .Check(u => u.ConfirmPassword).IsEqualTo("testing")
                .Check(u=>u.Manager.Username).IsNotEmpty();
        };
    }
}
