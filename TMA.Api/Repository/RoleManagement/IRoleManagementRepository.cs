using TMA.Api.Model;

namespace TMA.Api.Repository
{
    public interface IRoleManagementRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<IEnumerable<Actions>> GetActionsAsync();
        Task<IEnumerable<RoleAction>> GetRoleActionsAsync(int roleId);
        Task AddOrUpdateRoleActionAsync(int roleId, int actionId, bool hasFullAccess, bool hasReadOnly);
    }
}
