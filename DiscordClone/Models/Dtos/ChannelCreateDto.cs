namespace DiscordClone.Models.Dtos
{
    public class ChannelCreateDto
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public string? ChannelType { get; set; }
        public string? Topic { get; set; }
    }
}