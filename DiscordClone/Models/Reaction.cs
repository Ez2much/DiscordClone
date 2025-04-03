using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Reaction
    {
        [Key]
        // Unique identifier for the reaction
        public Guid ReactionId { get; set; }

        // Type of the reaction (e.g., 👍, ❤️)
        [Required(ErrorMessage = "Reaction type is required.")]
        [StringLength(10, ErrorMessage = "Reaction type cannot exceed 10 characters.")]
        public string ReactionType { get; set; } = null!;

        // Date when the reaction was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key referencing the message to which the reaction was added
        [ForeignKey("Message")]
        public Guid MessageId { get; set; }

        // The message to which the reaction was added
        public Message Message { get; set; } = null!;

        // Foreign key referencing the user who added the reaction
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The user who added the reaction
        public User User { get; set; } = null!;
    }
}
