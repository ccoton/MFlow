using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using MFlow.Mvc;

namespace MFlow.Samples.Mvc.Models
{
    public class LoginModel : ValidatedModel<LoginModel>
    {
        public LoginModel()
        {
            // Use the base ValidatedModel class to define rules
            // Pass in true to load the validation rules from xml
            SetTarget(this, loadXmlRuleset:true);

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
