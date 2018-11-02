using Microsoft.EntityFrameworkCore;
using SecuringAngularApps.API.Model;

namespace SecuringAngularApps.API.Model
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {

        }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<MilestoneStatus> MilestoneStatuses { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("name=ProjectDbContext");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
