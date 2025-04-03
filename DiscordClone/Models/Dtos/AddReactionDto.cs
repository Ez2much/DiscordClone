using System.ComponentModel.DataAnnotations;

namespace DiscordClone.Models.Dtos
{
    public class AddReactionDto
    {
            [Required]
    public Guid MessageId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "Reaction type cannot exceed 10 characters.")]
    public string ReactionType { get; set; } = null!;
    }
}
