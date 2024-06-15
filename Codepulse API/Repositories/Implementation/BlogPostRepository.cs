using Codepulse_API.Data;
using Codepulse_API.Models.Domain;
using Codepulse_API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Codepulse_API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogpost= await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if(existingBlogpost != null)
            {
                dbContext.BlogPosts.Remove(existingBlogpost);
                await dbContext.SaveChangesAsync();
                return existingBlogpost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllasync()
        {
           return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost= await dbContext.BlogPosts.Include(x=>x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            // update blogpost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            // update categories
            existingBlogPost.Categories=blogPost.Categories;

           await dbContext.SaveChangesAsync();

            return blogPost;
        }

    }
}
