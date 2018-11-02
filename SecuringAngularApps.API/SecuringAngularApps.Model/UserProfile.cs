using System;
using System.Collections.Generic;

namespace SecuringAngularApps.Model
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserPermission> UserPermissions { get; set; }
    }
}
