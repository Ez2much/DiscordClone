using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class VoiceSession
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the voice session
        public Guid SessionId { get; set; }

        // Date when the user joined the voice session
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Date when the user left the voice session (nullable if ongoing)
        public DateTime? LeftAt { get; set; }

        // Foreign key referencing the channel in which the voice session took place
        [ForeignKey("Channel")]
        public Guid ChannelId { get; set; }

        // The channel in which the voice session took place
        public Channel Channel { get; set; } = null!;

        // Foreign key referencing the user participating in the voice session
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The user participating in the voice session
        public User User { get; set; } = null!;

        // Indicates if the user is muted during the voice session
        public bool IsMuted { get; set; } = false;
    }
}
