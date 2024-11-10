using TMA.Model;

namespace TMA.Api.Repository
{
    public interface ITaskRepository
    {
        Task<(IEnumerable<TaskDto> tasks, int count)> GetAllAsync(int page, int pageSize);
        Task<TaskDto> GetByIdAsync(int taskId);
        Task<TaskDto> CreateAsync(TaskDto task);
        Task<TaskDto> UpdateAsync(TaskDto task);
        Task<TaskDto> DeleteAsync(int taskId);
    }
}
