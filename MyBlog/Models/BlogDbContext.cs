using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MyBlog.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasAlternateKey(e => e.UserName);

                entity.HasMany(e => e.Posts).WithOne(e => e.User).HasForeignKey(e => e.UserId);
                entity.HasMany(e => e.Comments).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            });

            builder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Comments).WithOne(e => e.Post).HasForeignKey(e => e.PostId);
            });

            builder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
