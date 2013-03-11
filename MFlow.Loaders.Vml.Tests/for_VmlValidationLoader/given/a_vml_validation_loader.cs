using Machine.Specifications;
using MFlow.Core;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Loaders.Xml.Tests.for_VmlValidationLoader.given
{
    public class a_vml_validation_loader
    {
        protected static IFluentValidationLoader validation_loader;

        Establish context = () =>
        {
            validation_loader = new ValidationLoader().Create<Loaders.Vml.VmlValidationLoader>();
        };
    }

    public class User
    {
        public User()
        {
            Tasks = new List<string>();
            Users = new List<User>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }

        public ICollection<string> Tasks { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
