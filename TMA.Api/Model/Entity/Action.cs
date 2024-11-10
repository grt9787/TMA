namespace TMA.Api.Model
{
    public class Actions 
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
