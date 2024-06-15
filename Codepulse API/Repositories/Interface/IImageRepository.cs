using Codepulse_API.Models.Domain;

namespace Codepulse_API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file,BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}
