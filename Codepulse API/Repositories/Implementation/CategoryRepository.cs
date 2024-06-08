using Codepulse_API.Data;
using Codepulse_API.Models.Domain;
using Codepulse_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Codepulse_API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Clategory> CreateAsync(Clategory clategory)
        {
            await dbContext.Categories.AddAsync(clategory);
            await dbContext.SaveChangesAsync();

            return clategory;
        }

        public async Task<Clategory?> DeleteAsync(Guid id)
        {
            var existingCategory=await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory is null)
            {
                return null;
            }
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Clategory>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Clategory?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Clategory?> UpdateAsync(Clategory clategory)
        {
            var ExistingCategory=await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == clategory.Id);
            if (ExistingCategory != null)
            {
                dbContext.Entry(ExistingCategory).CurrentValues.SetValues(clategory);
                await dbContext.SaveChangesAsync();
                return clategory;
            }
            return null;
        }
    }
}
