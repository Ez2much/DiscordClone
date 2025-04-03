using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordClone.Models
{
    public class Attachment
    {
        [Key]
        public Guid AttachmentId { get; set; }

        public string AttachmentUrl { get; set; } = null!;

        // Change this to AttachmentTypeEnum
        public AttachmentTypeEnum AttachmentType { get; set; }

        [ForeignKey("Message")]
        public Guid MessageId { get; set; }

        public Message Message { get; set; } = null!;
    }
}
