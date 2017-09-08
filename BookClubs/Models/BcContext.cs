using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

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
            modelBuilder.Configurations.Add(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
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
        }
    }
}