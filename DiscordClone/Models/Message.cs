using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Message
    {
        [Key]
        // Unique identifier for the message
        public Guid MessageId { get; set; }

        // Content of the message
        [StringLength(2000, ErrorMessage = "Message content cannot exceed 2000 characters.")]
        public string Content { get; set; } = null!;

        // Date when the message was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Indicates if the message has been edited
        public bool IsEdited { get; set; } = false;

        // Foreign key referencing the user who sent the message
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The user who sent the message
        public User User { get; set; } = null!;

        // Foreign key referencing the channel where the message was sent
        [ForeignKey("Channel")]
        public Guid ChannelId { get; set; }

        // The channel where the message was sent
        public Channel Channel { get; set; } = null!;

        // Collection of reactions to the message
        public ICollection<Reaction>? Reactions { get; set; }

        // Collection of attachments (e.g., files, images) linked to the message
        public ICollection<Attachment>? Attachments { get; set; }

        // Collection of edit history records for the message
        public ICollection<MessageEditHistory>? EditHistory { get; set; }
    }
}
