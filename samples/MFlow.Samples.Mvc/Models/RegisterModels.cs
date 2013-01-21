using System.ComponentModel.DataAnnotations;
using MFlow.Mvc;
using MFlow.Core.Conditions.Enums;

namespace MFlow.Samples.Mvc.Models
{
    public class RegisterModel : ValidatedModel<RegisterModel>
    {
        public RegisterModel()
        {
            // Use the base ValidatedModel class to define rules
            // Pass in true to load the validation rules from xml
            SetTarget(this, loadRuleset:true, rulesetFile:"RegisterModel.validation.vml");
            Validator
                .Check(c=>c.Forenames, output: ConditionOutput.Warning).IsNotEmpty().Message("Enter your firstname and middle name if applicable")
                .Check(c=>c.Surname, output: ConditionOutput.Warning).IsNotEmpty().Message("Enter your surname")
                .Check(c=>c.Email, output: ConditionOutput.Warning).IsNotEmpty().Message("Enter your email address")
                .Check(c=>c.UserName, output: ConditionOutput.Warning).IsNotEmpty().Message("Enter your username, think of something cool")
                .Check(c=>c.Password, output: ConditionOutput.Warning).IsNotEmpty().Message("Enter your password, something secure!")
                .Check(c=>c.ConfirmPassword, output: ConditionOutput.Warning).IsNotEmpty().Message("Confirm your password, make sure it matches the password above");
        }

        [Display(Name = "Forenames")]
        public string Forenames
        {
            get;
            set;
        }

        [Display(Name = "Surname")]
        public string Surname
        {
            get;
            set;
        }

        [Display(Name = "Email address")]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "User name")]
        public string UserName
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
