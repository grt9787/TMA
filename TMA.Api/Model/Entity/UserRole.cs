using Microsoft.EntityFrameworkCore;

namespace TMA.Api.Model
{


    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual User User { get; set; }   
        public virtual Role Role { get; set; }
    }
}
