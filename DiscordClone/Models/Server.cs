using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Server
    {
        [Key]
        // Unique identifier for the server
        public Guid ServerId { get; set; }

        // Name of the server
        public string Name { get; set; } = null!;

        // Description of the server
        public string? Description { get; set; }

        // Date when the server was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // URL to the server's icon image
        public string? IconUrl { get; set; }

        // Indicates if the server is public or private
        public bool IsPublic { get; set; } = true;

        // Foreign key referencing the owner of the server
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }

        // Owner of the server
        public User Owner { get; set; } = null!;




        // Collection of channels within the server
        public ICollection<Channel>? Channels { get; set; }

        // Collection of server members
        public ICollection<ServerMember>? ServerMembers { get; set; }

        // Collection of roles available on the server
        public ICollection<Role>? Roles { get; set; }

        // Collection of invites to the server
        public ICollection<Invite>? Invites { get; set; }

        // Collection of bans on the server
        public ICollection<ServerBan>? Bans { get; set; }
    }
}
