namespace DiscordClone.Models.Dtos
{
    public class ReactionDto
    {
        public Guid UserId { get; set; }
        public string ReactionType { get; set; } = null!;
    }
}
