using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;

namespace HaberApp.API.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IGenericRepository<NewsTag> _newsTagRepository;
        private readonly IGenericRepository<Category> _categoryRepository;

        public NewsService(
            INewsRepository newsRepository,
            IGenericRepository<NewsTag> newsTagRepository,
            IGenericRepository<Category> categoryRepository)
        {
            _newsRepository = newsRepository;
            _newsTagRepository = newsTagRepository;
            _categoryRepository = categoryRepository;
        }

        // 1. Tüm Haberleri Listele
        public async Task<IEnumerable<NewsListDto>> GetAllNewsAsync()
        {
            var newsList = await _newsRepository.GetAllNewsWithDetailsAsync();

            return newsList.Select(n => new NewsListDto
            {
                Id = n.Id,
                Title = n.Title,
                Slug = n.Slug,
                ImageUrl = n.ImageUrl,
                CategoryName = n.Category.Name,
                AuthorName = n.Author.FullName,
                PublishedAt = n.PublishedAt
            }).ToList();
        }

        // 2. Kategoriye Göre Haberleri Listele (Yeni!)
        public async Task<IEnumerable<NewsListDto>> GetNewsByCategoryIdAsync(int categoryId)
        {
            var newsList = await _newsRepository.GetNewsByCategoryIdWithDetailsAsync(categoryId);

            return newsList.Select(n => new NewsListDto
            {
                Id = n.Id,
                Title = n.Title,
                Slug = n.Slug,
                ImageUrl = n.ImageUrl,
                CategoryName = n.Category.Name,
                AuthorName = n.Author.FullName,
                PublishedAt = n.PublishedAt
            }).ToList();
        }

        // 3. Tek Bir Haberin Detayını Getir (Yeni!)
        public async Task<NewsDetailDto> GetNewsByIdAsync(int id)
        {
            var news = await _newsRepository.GetNewsByIdWithDetailsAsync(id);
            if (news == null) return null;

            return new NewsDetailDto
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                ImageUrl = news.ImageUrl,
                CategoryName = news.Category.Name,
                AuthorName = news.Author.FullName,
                PublishedAt = news.PublishedAt
            };
        }

        // 4. Yeni Haber Ekle (Gelişmiş Kontrollü)
        public async Task AddNewsAsync(NewsCreateDto dto, int authorId)
        {
            // Kategori kontrolü
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Hata: Girdiğiniz Kategori ID'si veritabanında bulunamadı!", nameof(dto.CategoryId));
            }

            var newNews = new News
            {
                CategoryId = dto.CategoryId,
                AuthorId = authorId,
                Title = dto.Title,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
                Slug = dto.Title.ToLower().Replace(" ", "-").Replace("ş", "s").Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ö", "o").Replace("ç", "c"),
                PublishedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _newsRepository.AddAsync(newNews);
            await _newsRepository.SaveChangesAsync();

            // Etiketleri ekle
            if (dto.TagIds != null && dto.TagIds.Any())
            {
                foreach (var tagId in dto.TagIds)
                {
                    var newsTag = new NewsTag
                    {
                        NewsId = newNews.Id,
                        TagId = tagId
                    };
                    await _newsTagRepository.AddAsync(newsTag);
                }
                await _newsTagRepository.SaveChangesAsync();
            }
        }
    }
}