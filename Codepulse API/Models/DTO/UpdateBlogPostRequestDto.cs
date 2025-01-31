﻿namespace Codepulse_API.Models.DTO
{
    public class UpdateBlogPostRequestDto
    {
        public String Title { get; set; }
        public String ShortDescription { get; set; }
        public String Content { get; set; }
        public String FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public String Author { get; set; }
        public bool IsVisible { get; set; }

        public List<Guid> Categories { get; set; } = new List<Guid>();
    }
}
