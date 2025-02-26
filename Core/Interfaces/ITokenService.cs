using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}
