namespace TMA.Api.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string RoleDescription { get; set; }

        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
