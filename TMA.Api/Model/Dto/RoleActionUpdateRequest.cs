namespace TMA.Api.Model
{
    public class RoleActionUpdateRequest
    {
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public bool HasFullAccess { get; set; }
        public bool HasReadOnly { get; set; }
    }
}
