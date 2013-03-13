using System;
using MFlow.Core.Events;
using System.Collections.Generic;

namespace MFlow.Core.Tests.Supporting
{
    public class User
    {
        public User()
        {
            Tasks = new List<string>();
            Users = new List<User>();
        }

        public int? LockedOutCount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int LoginCount { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }

        public ICollection<string> Tasks { get; set; }
        public ICollection<User> Users { get; set; }
    }

    public class UserCreatedEvent : Event<User>
    {
        public UserCreatedEvent(User source)
            : base(source, true)
        {
        }
    }
}
