using System.Text.Json.Serialization;

namespace TMA.Model
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
