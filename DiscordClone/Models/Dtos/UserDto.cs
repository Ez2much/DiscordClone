namespace DiscordClone.Models.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Image { get; set; } = null!;
    }
}
