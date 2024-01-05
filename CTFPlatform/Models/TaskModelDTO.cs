namespace CTFPlatform.Models
{
    public class TaskModelDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;
        public int Cost { get; set; } = 1000;
        public string Attachments {  get; set; } = string.Empty;
        public string DeployedEnv {  get; set; } = string.Empty;
    }
}
