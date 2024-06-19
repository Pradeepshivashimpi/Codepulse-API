using Codepulse_API.Models.Domain;

namespace Codepulse_API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync (BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllasync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPost?> UpdateAsync (BlogPost blogPost);
        Task<BlogPost?> DeleteAsync (Guid id);

    }
}
