using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class ServerMember
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the server member
        public Guid ServerMemberId { get; set; }

        // Foreign key referencing the server
        [ForeignKey("Server")]
        public Guid ServerId { get; set; }

        // The server to which the member belongs
        public Server Server { get; set; } = null!;

        // Foreign key referencing the user
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The user who is a member of the server
        public User User { get; set; } = null!;

        // Date when the user joined the server
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Indicates if the user is muted on the server
        public bool IsMuted { get; set; } = false;
    }
}
