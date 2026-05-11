using HaberApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanı
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // n:n ilişkisi için ara tablo (NewsTag) yapılandırması
            modelBuilder.Entity<NewsTag>()
                .HasKey(nt => new { nt.NewsId, nt.TagId });

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.News)
                .WithMany(n => n.NewsTags)
                .HasForeignKey(nt => nt.NewsId);

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(nt => nt.TagId);

            modelBuilder.Entity<News>()
                .HasOne(n => n.Author)
                .WithMany(u => u.News)
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}