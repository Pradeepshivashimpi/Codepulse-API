
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Codepulse_API.Data
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "9e716b87-fa6b-4d0c-b2e7-9f73d28da9eb";
            var writerRoleId = "dedf408e-49da-4cc4-b3dc-b01eda58845b";

            // create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                  Id = readerRoleId,
                  Name = "Reader",
                  NormalizedName = "Reader".ToUpper(),
                  ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };


            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);


            // create an adminuser
            var adminUserId = "18871800-3831-45c7-9856-d5a88e20951d";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);


            // Give Roles to Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
