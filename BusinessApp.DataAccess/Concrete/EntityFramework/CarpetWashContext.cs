using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.DataAccess.Concrete.EntityFramework
{
    public class CarpetWashContext : IdentityDbContext<User, Role, string>
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }

        // Parameterless contsractor
        public CarpetWashContext() { }
        public CarpetWashContext(DbContextOptions<CarpetWashContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=mssql07.turhost.com;Initial Catalog=CarpetWash;User=Admin; Password=5tX$$4D!;");
                //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=CarpetWash;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BuildCategoryModel(modelBuilder);

            BuildCommentsModel(modelBuilder);

            BuildTagsModel(modelBuilder);

            BuildUsersModel(modelBuilder);

            BuildBannerModel(modelBuilder);

            BuildContentModel(modelBuilder);
        }

        #region utils
        private void BuildCategoryModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .HasMaxLength(64);

                entity.Property(e => e.Image)
                      .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                     .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                     .HasColumnType("datetime2");

                entity.HasOne(e => e.User)
                     .WithMany(e => e.Categories)
                     .HasForeignKey(e => e.UserId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Categories_User");
            });
        }
        private void BuildCommentsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .HasMaxLength(64)
                      .HasColumnType("text");

                entity.Property(e => e.Email)
                      .HasMaxLength(64)
                      .HasColumnType("text");

                entity.Property(e => e.Text)
                      .HasMaxLength(500)
                      .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                     .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2");

                entity.HasOne(e => e.Content)
                     .WithMany(e => e.Comments)
                     .HasForeignKey(e => e.ContentId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Comments_Content");

                entity.HasOne(e => e.User)
                     .WithMany(e => e.Comments)
                     .HasForeignKey(e => e.UserId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Comments_User");
            });
        }
        private void BuildContentModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                      .HasMaxLength(250)
                      .HasColumnType("text");

                entity.Property(e => e.Slug)
                    .HasMaxLength(250)
                    .HasColumnType("text");

                entity.Property(e => e.Text)
                      .HasMaxLength(1024)
                      .HasColumnType("text");

                entity.Property(e => e.Excerpt)
                      .HasMaxLength(500)
                      .HasColumnType("text");

                entity.Property(e => e.Image)
                      .HasMaxLength(512)
                      .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                      .HasColumnType("datetime2");

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Contents)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Contents_User");

                entity.HasOne(e => e.Category)
                      .WithMany(e => e.Contents)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Contents_Category");
            });
        }
        private void BuildTagsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Id)
                     .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .HasMaxLength(32)
                      .HasColumnType("text");

                entity.Property(e => e.ItemType)
                      .HasMaxLength(32)
                      .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                     .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2");

                entity.HasOne(e => e.Content)
                      .WithMany(e => e.Tags)
                      .HasForeignKey(e => e.ContentId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Tags_Content");
            });
        }
        private void BuildUsersModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                     .ValueGeneratedOnAdd();

                entity.Property(e => e.UserName)
                      .HasMaxLength(32)
                      .HasColumnType("text");

                entity.Property(e => e.Email)
                      .HasMaxLength(32)
                      .HasColumnType("text");

                entity.Property(e => e.Biography)
                      .HasMaxLength(300)
                      .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                                     .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                                      .HasColumnType("datetime2");
            });
        }
        private void BuildBannerModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

                entity.Property(e => e.Image)
                     .HasMaxLength(255);

                entity.Property(e => e.Title)
                      .HasMaxLength(100);

                entity.Property(e => e.SubTitle)
                      .HasMaxLength(300);

                entity.Property(e => e.BtnTitle)
                      .HasMaxLength(64);

                entity.Property(e => e.CreatedAt)
                     .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                     .HasColumnType("datetime2");

                entity.HasOne(d => d.User)
                     .WithMany(p => p.Banners)
                     .HasForeignKey(d => d.UserId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Banners_User");
            });
        }
        #endregion
    }
}
