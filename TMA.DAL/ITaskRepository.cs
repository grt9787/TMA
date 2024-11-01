using TMA.Model;

namespace TMA.DAL
{
    public interface ITaskRepository
    {
        (IEnumerable<TaskDto> tasks, int count) GetAllTasks(int page, int pageSize);
        TaskDto GetTaskById(int id);
        void AddTask(TaskDto task);
        TaskDto UpdateTask(TaskDto updatedTask);
        TaskDto DeleteTask(int id);
        List<TaskDto> ReadTaskFromFile();
    }
}
