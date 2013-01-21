
using System;

namespace MFlow.Performance.Supporting
{
    /// <summary>
    ///     Description of User.
    /// </summary>
    public class User
    {
        public User ()
        {
        }
		
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
