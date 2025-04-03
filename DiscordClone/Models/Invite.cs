using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Invite
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the invite
        public Guid InviteId { get; set; }

        // Unique code for the invite
        public string Code { get; set; } = Guid.NewGuid().ToString();

        // Date when the invite was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Expiration date for the invite (nullable if no expiration)
        public DateTime? ExpirationDate { get; set; }

        // Foreign key referencing the server to which the invite applies
        [ForeignKey("Server")]
        public Guid ServerId { get; set; }

        // The server to which the invite applies
        public Server Server { get; set; } = null!;

        // Foreign key referencing the user who created the invite
        [ForeignKey("Inviter")]
        public Guid InviterId { get; set; }

        // The user who created the invite
        public User Inviter { get; set; } = null!;
    }
}
