using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookClubs.Models
{
    public class BcContext : IdentityDbContext<User>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<Book> Books { get; set; }

        public BcContext() : base("DefaultConnection", throwIfV1Schema: false) { }
        public static BcContext Create()
        {
            return new BcContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new GroupWallPostConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new FriendRequestConfiguration());
            modelBuilder.Configurations.Add(new GroupEventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    internal class GroupEventConfiguration : EntityTypeConfiguration<GroupEvent>
    {
        public GroupEventConfiguration()
        {
            HasRequired(e => e.Book)
                .WithMany(b => b.GroupEvents)
                .HasForeignKey(e => e.BookId);
        }
    }

    internal class FriendRequestConfiguration : EntityTypeConfiguration<FriendRequest>
    {
        public FriendRequestConfiguration()
        {
            HasRequired(r => r.Sender)
                .WithMany(u => u.SentRequests)
                .HasForeignKey(r => r.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.Recipient)
                .WithMany(u => u.PendingRequests)
                .HasForeignKey(r => r.RecipientId)
                .WillCascadeOnDelete(true);
        }
    }

    internal class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            HasKey(b => b.Isbn);
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
                .HasForeignKey(p => p.PosterId);
        }
    }

    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Friends)
                .WithMany().Map(u =>
                {
                    u.ToTable("Friends");
                });

            //HasMany(u => u.PendingRequests)
            //    .WithRequired(r => r.Recipient);

            //HasMany(u => u.SentRequests)
            //    .WithRequired(r => r.Sender);
        }
    }
}