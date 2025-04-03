using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class UserRole
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the user-role assignment
        public Guid UserRoleId { get; set; }

        // Foreign key referencing the user
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The user to whom the role is assigned
        public User User { get; set; } = null!;

        // Foreign key referencing the role
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        // The role assigned to the user
        public Role Role { get; set; } = null!;

        // Foreign key referencing the server where the role is assigned
        [ForeignKey("Server")]
        public Guid ServerId { get; set; }

        public Server Server { get; set; } = null!;
    }
}