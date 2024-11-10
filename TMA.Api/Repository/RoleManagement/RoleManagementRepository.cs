using Microsoft.EntityFrameworkCore;
using TMA.Api.Model;

namespace TMA.Api.Repository
{
    public class RoleManagementRepository : IRoleManagementRepository
    {
        private readonly TaskContext _context;

        public RoleManagementRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _context.Roles.Where(r => !r.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Actions>> GetActionsAsync()
        {
            return await _context.Actions.ToListAsync();
        }

        public async Task<IEnumerable<RoleAction>> GetRoleActionsAsync(int roleId)
        {
            return await _context.RoleActions
                                 .Where(ra => ra.RoleId == roleId)
                                 .Include(ra => ra.Action)
                                 .ToListAsync();
        }

        public async Task AddOrUpdateRoleActionAsync(int roleId, int actionId, bool hasFullAccess, bool hasReadOnly)
        {
            var roleAction = await _context.RoleActions
                                           .FirstOrDefaultAsync(ra => ra.RoleId == roleId && ra.ActionId == actionId);
            if (roleAction == null)
            {
                roleAction = new RoleAction
                {
                    RoleId = roleId,
                    ActionId = actionId,
                    HasFullAccess = hasFullAccess,
                    HasReadOnly = hasReadOnly,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.UtcNow
                };
                await _context.RoleActions.AddAsync(roleAction);
            }
            else
            {
                roleAction.HasFullAccess = hasFullAccess;
                roleAction.HasReadOnly = hasReadOnly;
                roleAction.ModifiedBy = "admin";
                roleAction.ModifiedDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
