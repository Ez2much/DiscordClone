namespace DiscordClone.Models.Dtos
{
    public class ServerCreateDto
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public bool IsPublic { get; set; }
    }
}
