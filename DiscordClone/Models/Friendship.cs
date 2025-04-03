using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{

    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public class Friendship
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }

        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }

        public FriendshipStatus Status { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public DateTime? AcceptedAt { get; set; }

        public virtual User? Sender { get; set; }
        public virtual User? Receiver { get; set; }

    }
}
