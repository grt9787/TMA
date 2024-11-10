using System.Security.Claims;
using TMA.Api.Model;

namespace TMA.Api.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<List<Claim>> GetUserClaimsAsync(User user);
    }
}
