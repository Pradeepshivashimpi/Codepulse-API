using Codepulse_API.Models.Domain;

namespace Codepulse_API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Clategory> CreateAsync(Clategory clategory);
        Task<IEnumerable<Clategory>> GetAllAsync(string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);

        Task<Clategory?>GetById(Guid id);
        Task<Clategory?> UpdateAsync(Clategory clategory);
        Task<Clategory?> DeleteAsync(Guid id);
        Task<int> GetCount();
    }
}
