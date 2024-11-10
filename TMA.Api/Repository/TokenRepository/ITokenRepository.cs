using System.Security.Claims;
using TMA.Api.Model;

namespace TMA.Api.Repository
{
    public interface ITokenRepository
    {
        TokenResponse GenerateToken(User user, List<Claim> claims);
    }
}
