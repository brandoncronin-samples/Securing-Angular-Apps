using System;
using System.Collections.Generic;

namespace SecuringAngularApps.API.Model
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasLoggedIn { get; set; }

        public List<UserPermission> UserPermissions { get; set; }
    }
}
