using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class PollOption
    {
        //tabela posrednia
        [Key]
        // Unique identifier for the poll option
        public Guid PollOptionId { get; set; }

        // Text of the poll option
        [Required(ErrorMessage = "Option text is required.")]
        [StringLength(200, ErrorMessage = "Option text cannot exceed 200 characters.")]
        public string OptionText { get; set; } = null!;

        // Foreign key referencing the poll to which the option belongs
        [ForeignKey("Poll")]
        public Guid PollId { get; set; }

        // The poll to which the option belongs
        public Poll Poll { get; set; } = null!;
    }
}
