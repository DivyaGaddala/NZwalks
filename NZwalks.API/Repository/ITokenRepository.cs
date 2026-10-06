using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repository
{
    public interface ITokenRepository
    {

        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
