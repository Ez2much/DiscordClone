using System.ComponentModel.DataAnnotations;


namespace DiscordClone.Models.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "User"; // Default role is "User"
    }
}