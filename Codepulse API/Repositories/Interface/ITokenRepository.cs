using Microsoft.AspNetCore.Identity;

namespace Codepulse_API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user,List<string> roles);
    }
}
