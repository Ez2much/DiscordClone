using System.ComponentModel.DataAnnotations;

namespace DiscordClone.Models.Dtos
{
    public class FriendRequestDto
    {
        public Guid requestId { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid ReceiverId { get; set; }
        public string UserName { get; set; }
        public string? Image { get; set; }
    }
}
