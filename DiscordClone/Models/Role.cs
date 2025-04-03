using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Role
    {
        [Key]
        // Unique identifier for the role
        public Guid RoleId { get; set; }

        // Name of the role
        public string Name { get; set; } = null!;

        // Permissions associated with the role (comma-separated list)
        public string Permissions { get; set; } = null!;

        // Date when the role was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Color associated with the role (e.g., for UI purposes)
        public string? Color { get; set; }

        // Foreign key referencing the server to which the role belongs
        [ForeignKey("Server")]
        public Guid ServerId { get; set; }

        // The server to which the role belongs
        public Server Server { get; set; } = null!;

        // Collection of user-role assignments
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
