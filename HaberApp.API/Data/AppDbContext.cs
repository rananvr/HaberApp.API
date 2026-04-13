using HaberApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.API.Data
{
    // DbContext'ten miras alıyoruz ki EF Core'un tüm özelliklerini kullanalım
    public class AppDbContext : DbContext
    {
        // Constructor (Yapıcı Metot) - Bağlantı ayarlarını içeri almak için
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanında oluşacak tablolarımız (DbSet'ler)
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        // Veritabanı kurallarını (ilişkileri) burada detaylandırıyoruz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. KURAL: NewsTag (Haber-Etiket) çoka çok ilişkisi için çiftil anahtar (Primary Key) oluşturuyoruz.
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

            // 2. KURAL: Bir yazar (User) silindiğinde, yazdığı tüm haberler SİLİNMESİN.
            // Çünkü haberler uygulamada kalmalı. (Restrict)
            modelBuilder.Entity<News>()
                .HasOne(n => n.Author)
                .WithMany(u => u.News)
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. KURAL: Yorum yapan bir kullanıcı silinirse, haberin altındaki yorumları da silinsin. (Cascade - Varsayılandır ama belirtebiliriz)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}