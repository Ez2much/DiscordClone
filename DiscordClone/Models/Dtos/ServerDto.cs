namespace DiscordClone.Models.Dtos
{
    public class ServerDto
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public bool IsPublic { get; set; }
    }
}