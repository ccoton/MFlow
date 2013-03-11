using System.ComponentModel.DataAnnotations;
using MFlow.Mvc;
using MFlow.Core;
using MFlow.Loaders.Vml;

namespace MFlow.Samples.Mvc.Models
{
    public class LoginModel : ValidatedModel<LoginModel>
    {
        public LoginModel()
        {
            // Use the base ValidatedModel class to define rules
            // Pass in true to load the validation rules from xml
            GetValidator(this, rulesetFile: "VmlConfiguration/LoginModel.validation.vml", 
                loader: () => { return new ValidationLoader().Create<VmlValidationLoader>(); });

            //Validator
            //    .NotEmpty(m => m.UserName, message: "Username cannot be empty")
            //    .NotEmpty(m => m.Password, message: "Password cannot be empty")
            //    .IsEmail(m => m.UserName, message: "Username should be an email address");
        }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
