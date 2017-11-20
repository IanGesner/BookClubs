using BookClubs.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace BookClubs.Data.Configuration
{
    public class BcContext : IdentityDbContext<User>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
        public BcContext() : base("DefaultConnection", throwIfV1Schema: false) { }
        public static BcContext Create()
        {
            return new BcContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new GroupWallPostConfiguration());
            modelBuilder.Configurations.Add(new GroupWallPostReplyConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new FriendRequestConfiguration());
            modelBuilder.Configurations.Add(new GroupEventConfiguration());
            modelBuilder.Configurations.Add(new GroupInvitationConfiguration());
            modelBuilder.Configurations.Add(new GroupRequestConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new AuthorConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    internal class GroupWallPostReplyConfiguration : EntityTypeConfiguration<GroupWallPostReply>
    {
        public GroupWallPostReplyConfiguration()
        {            
            HasRequired(pr => pr.Poster)
                .WithMany(p => p.GroupWallPostReplies)
                .HasForeignKey(pr => pr.PosterId);

            HasRequired(pr => pr.OriginalPost)
                .WithMany(p => p.Replies);
        }
    }

    internal class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(a => a.FirstName)
                .HasMaxLength((int)MaxLength.FirstName)
                .IsRequired();

            Property(a => a.LastName)
                .HasMaxLength((int)MaxLength.FirstName)
                .IsRequired();
        }
    }

    internal class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasRequired(g => g.Organizer)
                .WithMany(u => u.GroupsOrganized)
                .HasForeignKey(g => g.OrganizerId)
                .WillCascadeOnDelete(false);

            HasMany(g => g.Users)
                .WithMany(g => g.GroupsIn);

            Property(g => g.Name)
                .HasMaxLength((int)MaxLength.GroupName)
                .IsRequired();

            Property(g => g.Name)
                .HasMaxLength((int)MaxLength.City)
                .IsRequired();

            Property(g => g.Name)
                .HasMaxLength((int)MaxLength.State)
                .IsRequired();

            Property(g => g.GroupInfo)
                .HasMaxLength((int)MaxLength.MessageBody);
        }
    }

    internal class GroupRequestConfiguration : EntityTypeConfiguration<GroupRequest>
    {
        public GroupRequestConfiguration()
        {
            HasRequired(r => r.Sender)
                .WithMany(u => u.SentGroupRequests)
                .HasForeignKey(r => r.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.Recipient)
                .WithMany(u => u.PendingGroupRequests)
                .HasForeignKey(r => r.RecipientId)
                .WillCascadeOnDelete(true);

            HasRequired(r => r.Group)
                .WithMany(g => g.GroupRequests)
                .HasForeignKey(r => r.GroupId);

            Property(r => r.Body)
                .HasMaxLength((int)MaxLength.MessageBody);
        }
    }

    internal class GroupInvitationConfiguration : EntityTypeConfiguration<GroupInvitation>
    {
        public GroupInvitationConfiguration()
        {
            HasRequired(i => i.Sender)
                .WithMany(u => u.SentGroupInvitations)
                .HasForeignKey(r => r.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.Recipient)
                .WithMany(u => u.PendingGroupInvitations)
                .HasForeignKey(i => i.RecipientId)
                .WillCascadeOnDelete(true);

            HasRequired(i => i.Group)
                .WithMany(g => g.GroupInvitations)
                .HasForeignKey(i => i.GroupId);

            Property(g => g.Body)
                .HasMaxLength((int)MaxLength.MessageBody);
        }
    }

    internal class GroupEventConfiguration : EntityTypeConfiguration<GroupEvent>
    {
        public GroupEventConfiguration()
        {
            HasRequired(e => e.Book)
                .WithMany(b => b.GroupEvents)
                .HasForeignKey(e => e.BookId);

            HasRequired(e => e.Group)
                .WithMany(g => g.GroupEvents)
                .HasForeignKey(e => e.GroupId);

            Property(e => e.Address)
                .HasMaxLength((int)MaxLength.Address)
                .IsRequired();

            Property(e => e.City)
                .HasMaxLength((int)MaxLength.City)
                .IsRequired();

            Property(e => e.State)
                .HasMaxLength((int)MaxLength.State)
                .IsRequired();

            Property(e => e.ZipCode)
                .HasMaxLength((int)MaxLength.ZipCode)
                .IsRequired();
        }
    }

    internal class FriendRequestConfiguration : EntityTypeConfiguration<FriendRequest>
    {
        public FriendRequestConfiguration()
        {
            HasRequired(r => r.Sender)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(r => r.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.Recipient)
                .WithMany(u => u.PendingFriendRequests)
                .HasForeignKey(r => r.RecipientId)
                .WillCascadeOnDelete(true);

            Property(r => r.Body)
                .HasMaxLength((int)MaxLength.MessageBody)
                .IsRequired();
        }
    }

    internal class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            HasKey(b => b.Isbn);
            Property(b => b.Isbn)
                .HasMaxLength((int)MaxLength.Isbn);

            Property(b => b.Title)
                .HasMaxLength((int)MaxLength.Title)
                .IsRequired();
        }
    }

    internal class GroupWallPostConfiguration : EntityTypeConfiguration<GroupWallPost>
    {
        public GroupWallPostConfiguration()
        {
            HasRequired(p => p.Group)
                .WithMany(g => g.GroupWallPosts)
                .HasForeignKey(p => p.GroupId);

            HasRequired(p => p.Poster)
                .WithMany(u => u.GroupWallPosts)
                .HasForeignKey(p => p.PosterId)
                .WillCascadeOnDelete(false);

            Property(p => p.Body)
                .HasMaxLength((int)MaxLength.MessageBody)
                .IsRequired();

            //HasMany(p => p.Replies)
            //    .WithOptional(r => r.OriginalPost)
            //    .HasForeignKey(r => r.OriginalPostId);
        }
    }

    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Friends)
                .WithMany()
                .Map(u =>
                {
                    u.ToTable("Friends");
                });

            Property(u => u.FirstName)
                .HasMaxLength((int)MaxLength.FirstName)
                .IsRequired();

            Property(u => u.LastName)
                .HasMaxLength((int)MaxLength.LastName)
                .IsRequired();

            Property(u => u.Biography)
                .HasMaxLength((int)MaxLength.Biography);

            Property(u => u.Public)
                .IsRequired();
        }
    }

    internal enum MaxLength
    {
        ZipCode = 5,
        Isbn = 13,
        FirstName = 64,
        LastName = 64,
        GroupName = 64,
        MessageBody = 1024,
        Biography = 1024,
        Address = 128,
        Title = 128,
        City = 64,
        State = 64,
    }
}

