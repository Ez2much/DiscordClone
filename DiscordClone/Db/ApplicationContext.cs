
using DiscordClone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscordClone.Db
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public DbSet<User> appUsers { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<ServerMember> ServerMembers { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<VoiceSession> VoiceSessions { get; set; }
        public DbSet<ChannelPermission> ChannelPermissions { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Role> ServersRoles { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<ServerBan> ServerBans { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<UserRole> UserServerRole { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<PollVote> PollVotes { get; set; }
        public DbSet<MessageEditHistory> messageEditHistories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<PinnedMessage> PinnedMessages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Server>()
                .HasOne(s => s.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DirectMessage>()
                .HasOne(dm => dm.Sender)
                .WithMany(u => u.SentDirectMessages)
                .HasForeignKey(dm => dm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DirectMessage>()
                .HasOne(dm => dm.Receiver)
                .WithMany(u => u.ReceivedDirectMessages)
                .HasForeignKey(dm => dm.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasMany(u => u.SentFriendRequests)
            .WithOne(f => f.Sender)
            .HasForeignKey(f => f.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedFriendRequests)
                .WithOne(f => f.Receiver)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
