using Microsoft.AspNetCore.Identity;

namespace DiscordClone.Models
{
    public class User : IdentityUser<Guid>
    {

        // URL to the user's avatar image
        public string? AvatarUrl { get; set; }

        // Date when the user account was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Short biography of the user
        public string? Bio { get; set; }

        // Online status of the user (true if online, false otherwise)
        public bool IsOnline { get; set; } = false;

        public ICollection<Friendship>? SentFriendRequests { get; set; }
        public ICollection<Friendship>? ReceivedFriendRequests { get; set; }

        // Collection of server memberships for the user
        public ICollection<ServerMember>? ServerMembers { get; set; }

        // Collection of messages sent by the user in channels
        public ICollection<Message>? Messages { get; set; }

        // Collection of direct messages sent by the user
        public ICollection<DirectMessage>? SentDirectMessages { get; set; }

        // Collection of direct messages received by the user
        public ICollection<DirectMessage>? ReceivedDirectMessages { get; set; }

        // Collection of reactions made by the user
        public ICollection<Reaction>? Reactions { get; set; }

        // Collection of voice sessions in which the user has participated
        public ICollection<VoiceSession>? VoiceSessions { get; set; }

        // Collection of activity logs for the user
        public ICollection<UserActivityLog>? ActivityLogs { get; set; }
    }
}
