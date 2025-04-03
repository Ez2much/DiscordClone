using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class UserActivityLog
    {
        [Key]
        // Unique identifier for the user activity log
        public Guid ActivityLogId { get; set; }

        // Type of activity (e.g., "Joined Server", "Sent Message")
        public string ActivityType { get; set; } = null!;

        // Timestamp of when the activity occurred
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Foreign key referencing the user who performed the activity
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The user who performed the activity
        public User User { get; set; } = null!;
    }
}
