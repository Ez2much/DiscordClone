using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class ServerBan
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the server ban
        public Guid BanId { get; set; }

        // Date when the user was banned
        public DateTime BannedAt { get; set; } = DateTime.UtcNow;

        // Reason for the ban
        public string Reason { get; set; } = null!;

        // Foreign key referencing the server from which the user is banned
        [ForeignKey("Server")]
        public Guid ServerId { get; set; }

        // The server from which the user is banned
        public Server Server { get; set; } = null!;

        [ForeignKey("BanningUser")]
        public Guid BanningUserId { get; set; }

        // The user who is banning
        public User BanningUser { get; set; } = null!;

        // Foreign key referencing the banned user
        [ForeignKey("User")]
        public Guid BannedUserId { get; set; }

        // The user who is banned
        public User BannedUser { get; set; } = null!;
    }
}