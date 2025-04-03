using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Poll
    {
        [Key]
        // Unique identifier for the poll
        public Guid PollId { get; set; }

        // Question for the poll
        [Required(ErrorMessage = "The poll question is required.")]
        [StringLength(500, ErrorMessage = "The poll question cannot exceed 500 characters.")]
        public string Question { get; set; } = null!;

        // Date when the poll was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Expiration date for the poll (nullable if no expiration)
        public DateTime? ExpirationDate { get; set; }

        // Foreign key referencing the channel in which the poll is conducted
        [ForeignKey("Channel")]
        public Guid ChannelId { get; set; }

        // The channel in which the poll is conducted
        public Channel Channel { get; set; } = null!;

        // Collection of options for the poll
        public ICollection<PollOption>? Options { get; set; }

        // Collection of votes for the poll
        public ICollection<PollVote>? Votes { get; set; }
    }
}
