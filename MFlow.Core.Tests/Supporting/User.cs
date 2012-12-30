using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Events;

namespace MFlow.Core.Tests.Supporting
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class UserCreatedEvent : Event<User>
    {
        public UserCreatedEvent(User source)
            : base(source, true)
        {
        }
    }
}
