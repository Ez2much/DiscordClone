using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class PinnedMessage
    {
        [Key]
        public Guid PinnedMessageId { get; set; }

        [ForeignKey("Message")]
        public Guid MessageId { get; set; } // ID of the message being pinned
        public Message Message { get; set; } // Navigation property to the Message entity

        [ForeignKey("Channel")]
        public Guid ChannelId { get; set; } // ID of the channel where the message is pinned
        public Channel Channel { get; set; } // Navigation property to the Channel entity

        [ForeignKey("PinnedBy")]
        public Guid PinnedById { get; set; } // ID of the user who pinned the message
        public User PinnedBy { get; set; } // Navigation property to the User entity who pinned the message

        public DateTime PinnedAt { get; set; } = DateTime.UtcNow; // Date and time the message was pinned
    }
}
