﻿using Codepulse_API.Data;
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

        public async Task<IEnumerable<Clategory>> GetAllAsync(string? query=null,
            string? sortBy = null, 
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100)
        {
            var categories = dbContext.Categories.AsQueryable();

            // Filtering

            if (string.IsNullOrWhiteSpace(query)==false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }


            // Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(string.Equals(sortBy,"Name",StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase)
                        ? true : false;
                    categories = isAsc ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x=>x.Name);
                }

                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase)
                        ? true : false;
                    categories = isAsc ? categories.OrderBy(x => x.UrlHandle) : categories.OrderByDescending(x => x.UrlHandle);
                }
            }


            // Pagination
            // page number 1 page size 5 - skip 0 take 5
            // page number 2 page size 5 - skip 5 take 5
            // page number 3 page size 5 - skip 10 take 5

            var skipResults = (pageNumber-1) * pageSize;
            categories = categories.Skip(skipResults ?? 0).Take(pageSize ?? 100);



            return await categories.ToListAsync();

        }

        public async Task<Clategory?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount()
        {
            return await dbContext.Categories.CountAsync();
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
