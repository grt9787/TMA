using System.Security.Claims;

namespace TMA.Api.Model
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
