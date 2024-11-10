namespace TMA.Api.Model
{
    public class RoleAction
    {
        public int RoleActionId { get; set; }
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public bool HasFullAccess { get; set; }
        public bool HasReadOnly { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual Actions Action { get; set; }
    }
}
