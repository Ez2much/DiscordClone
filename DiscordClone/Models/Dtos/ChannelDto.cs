namespace DiscordClone.Models.Dtos
{
    public class ChannelDto
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public string ChannelType { get; set; }
        public string? Topic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}