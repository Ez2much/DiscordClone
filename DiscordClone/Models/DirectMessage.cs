using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class DirectMessage
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the direct message
        public Guid DirectMessageId { get; set; }

        // Content of the direct message
        [StringLength(2000, ErrorMessage = "Message content cannot exceed 2000 characters.")]
        public string Content { get; set; } = null!;

        // Date when the direct message was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Indicates if the direct message has been read by the recipient
        public bool IsRead { get; set; } = false;

        // Foreign key referencing the sender of the direct message
        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }

        // The user who sent the direct message
        public User Sender { get; set; } = null!;

        // Foreign key referencing the receiver of the direct message
        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }

        // The user who received the direct message
        public User Receiver { get; set; } = null!;
    }
}
