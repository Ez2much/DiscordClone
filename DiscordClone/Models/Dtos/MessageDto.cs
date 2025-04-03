namespace DiscordClone.Models.Dtos
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public Guid ChannelId { get; set; }

        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;

        public List<ReactionDto> Reactions { get; set; } = new List<ReactionDto>();
    }
}