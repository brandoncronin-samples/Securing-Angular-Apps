using System.Collections.Generic;

namespace SecuringAngularApps.API.Model
{
    public class AuthContext
    {
        public UserProfile UserProfile { get; set; }
        public List<SimpleClaim> Claims { get; set; }
    }
}
