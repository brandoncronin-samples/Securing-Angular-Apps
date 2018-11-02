using System;
using System.Collections.Generic;

namespace SecuringAngularApps.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Milestone> Milestones { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
