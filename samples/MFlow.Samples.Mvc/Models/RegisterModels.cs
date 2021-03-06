﻿using System.ComponentModel.DataAnnotations;
using MFlow.Mvc;
using MFlow.Core.Conditions.Enums;
using MFlow.Core;
using MFlow.Loaders.Vml;

namespace MFlow.Samples.Mvc.Models
{
    public class RegisterModel : ValidatedModel<RegisterModel>
    {
        public RegisterModel()
        {
            // Use the base ValidatedModel class to define rules
            // Pass in true to load the validation rules from xml
            GetValidator(this, rulesetFile: "VmlConfiguration/RegisterModel.validation.vml", 
                loader: () => { return new ValidationLoader().Create<VmlValidationLoader>(); });
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
