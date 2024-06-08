using Codepulse_API.Models.Domain;

namespace Codepulse_API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync (BlogPost blogPost);
    }
}
