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
            SetTarget(this);
            Validator
                .NotNullOrEmpty(m => m.UserName, message: "Username cannot be empty")
                .NotNullOrEmpty(m => m.Password, message: "Password cannot be empty")
                .RegEx(m => m.UserName, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", message: "Username should be an email address")
                .NotEqual(m => m.UserName, "admin@domain.com", message: "What are you doing?");
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
