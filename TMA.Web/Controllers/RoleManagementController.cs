using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TMA.Api.Model;
using TMA.Api.Repository;

namespace TMA.Web.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class RoleManagementController : ControllerBase
    {
        private readonly IRoleManagementRepository _roleManagementRepository;

        public RoleManagementController(IRoleManagementRepository roleManagementRepository)
        {
            _roleManagementRepository = roleManagementRepository;
        }


        /// <summary>
        /// Retrieves a list of roles.
        /// </summary>
        /// <returns>A list of roles.</returns>

        [HttpGet("roles")]
        [SwaggerOperation(Summary = "Get Roles", Description = "", OperationId = "GetRoles", Tags = new[] { "admin" })]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManagementRepository.GetRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Retrieves a list of actions.
        /// </summary>
        /// <returns>A list of actions.</returns>

        [HttpGet("actions")]
        [SwaggerOperation(Summary = "Get Actions", Description = "", OperationId = "GetActions", Tags = new[] { "admin" })]
        public async Task<IActionResult> GetActions()
        {
            var actions = await _roleManagementRepository.GetActionsAsync();
            return Ok(actions);
        }

        /// <summary>
        /// Retrieves a list of roleActions.
        /// </summary>
        /// <returns>A list of roleActions.</returns>

        [HttpGet("roleActions")]
        [SwaggerOperation(Summary = "Get Roles Actions", Description = "", OperationId = "GetRoleActions", Tags = new[] { "admin" })]
        public async Task<IActionResult> GetRoleActions(int roleId)
        {
            var roleActions = await _roleManagementRepository.GetRoleActionsAsync(roleId);
            return Ok(roleActions);
        }


        /// <summary>
        /// Updates an existing roleAction.
        /// </summary>
        /// <param name="updatedRoleAction">The roleAction data to update.</param>
        /// <returns>The updated roleAction.</returns>
        [HttpPost("updateRoleAction")]
        [SwaggerOperation(Summary = "Update Role Action", Description = "", OperationId = "UpdateRoleAction", Tags = new[] { "admin" })]
        public async Task<IActionResult> UpdateRoleAction([FromBody] RoleActionUpdateRequest request)
        {
            await _roleManagementRepository.AddOrUpdateRoleActionAsync(
                request.RoleId,
                request.ActionId,
                request.HasFullAccess,
                request.HasReadOnly
            );
            return Ok();
        }
    }
}
