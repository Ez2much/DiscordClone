using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class MessageEditHistory
    {

        [Key]
        // Unique identifier for the edit history record
        public Guid EditHistoryId { get; set; }

        // The content of the message before the edit
        public string OldContent { get; set; } = null!;

        // Date when the message was edited
        public DateTime EditedAt { get; set; } = DateTime.UtcNow;

        // Foreign key referencing the message that was edited
        [ForeignKey("Message")]
        public Guid MessageId { get; set; }

        // The message that was edited
        public Message Message { get; set; } = null!;
    }
}
