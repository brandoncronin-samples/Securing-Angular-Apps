namespace SecuringAngularApps.API.Model
{
    public class Milestone
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int MilestoneStatusId { get; set; }
        public MilestoneStatus MilestoneStatus { get; set; }
    }
}
