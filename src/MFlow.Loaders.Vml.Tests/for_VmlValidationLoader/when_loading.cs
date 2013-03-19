using Machine.Specifications;
using MFlow.Core.Conditions;
using MFlow.Core.Validation;
using MFlow.Loaders.Xml.Tests.for_VmlValidationLoader.given;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Loaders.Xml.Tests.for_VmlValidationLoader
{
    public class when_loading : given.a_vml_validation_loader
    {
        static IFluentConditions<User> validation;

        Because of = () =>
        {
            validation = (IFluentConditions<User>)validation_loader.Load<User>(new User(), @"for_VmlValidationLoader\given\a_vml_validation_configuration.vml");
        };

        It should_load_after_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "AfterValidatorMessage"); };
        It should_load_before_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "BeforeValidatorMessage"); };
        It should_load_contains_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "ContainsValidatorMessage"); };
        It should_load_equal_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "EqualValidatorMessage"); };
        It should_load_equal_to_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "EqualToValidatorMessage"); };
        It should_load_greater_than_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "GreaterThanValidatorMessage"); };
        It should_load_greater_than_or_equal_to_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "GreaterThanOrEqualToValidatorMessage"); };
        It should_load_alpha_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "AlphaValidatorMessage"); };
        It should_load_credit_card_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "CreditCardValidatorMessage"); };
        It should_load_date_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsDateValidatorMessage"); };
        It should_load_email_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsEmailValidatorMessage"); };
        It should_load_length_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsLengthValidatorMessage"); };
        It should_load_longer_than_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "LongerThanValidatorMessage"); };
        It should_load_numeric_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsNumericValidatorMessage"); };
        It should_load_post_code_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsPostCodeValidatorMessage"); };
        It should_load_required_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsRequiredValidatorMessage"); };
        It should_load_shorter_than_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsShorterThanValidatorMessage"); };
        It should_load_this_month_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsThisMonthValidatorMessage"); };
        It should_load_this_week_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsThisWeekValidatorMessage"); };
        It should_load_this_year_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsThisYearValidatorMessage"); };
        It should_load_today_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsTodayValidatorMessage"); };
        It should_load_zip_code_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsZipCodeValidatorMessage"); };
        It should_load_less_than_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "LessThanValidatorMessage"); };
        It should_load_less_than_or_equal_to_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "LessThanOrEqualToValidatorMessage"); };
        It should_load_not_empty_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "NotEmptyValidatorMessage"); };
        It should_load_not_equal_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "NotEqualValidatorMessage"); };
        It should_load_not_equal_to_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "NotEqualToValidatorMessage"); };
        It should_load_on_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "OnValidatorMessage"); };
        It should_load_regex_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "RegExValidatorMessage"); };
        It should_load_is_not_null_validator = () => { validation.Conditions.ShouldContain(c => c.Message == "IsNotNullValidatorMessage"); };
    }
}