using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TMA.Api.Model;

namespace TMA.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskContext _context;

        public UserRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Email == email && u.Password == password && !u.IsDeleted);
        }

        public async Task<List<Claim>> GetUserClaimsAsync(User user)
        {
            var claims = new List<Claim>();

          
            claims.Add(new Claim("UserId", user.UserId.ToString()));
            claims.Add(new Claim("UserName", user.FirstName+""+user.LastName));

            // Get roles for the user
            var userRoles = await _context.UserRoles.Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.UserId)
               // .Select(ur => ur.Role)
                .ToListAsync();

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("UserRoleId", role.UserRoleId.ToString()));
                claims.Add(new Claim("RoleId", role.Role.RoleId.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));

                // Get actions associated with each role
                var roleActions = await _context.RoleActions
                    .Where(ra => ra.RoleId == role.Role.RoleId)
                    .ToListAsync();

                foreach (var action in roleActions)
                {
                    if (action.HasFullAccess)
                    {
                        claims.Add(new Claim("Permission", $"{role.Role.Name}.{action.ActionId}.FullAccess"));
                    }
                    if (action.HasReadOnly)
                    {
                        claims.Add(new Claim("Permission", $"{role.Role.Name}.{action.ActionId}.ReadOnly"));
                    }
                }
            }

            return claims;
        }
    }
}
