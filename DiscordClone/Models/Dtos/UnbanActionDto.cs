namespace DiscordClone.Models.Dtos
{
    public class UnbanActionDto
    {
        public Guid ServerId { get; set; }
        public Guid RemoverId { get; set; }
        public Guid BannedUserId { get; set; }
    }
}
