using System.Text.Json;
using TMA.Model;

namespace TMA.DAL
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _filePath = "tasks.json";

        public TaskRepository(){}

        public (IEnumerable<TaskDto> tasks, int count) GetAllTasks(int page, int pageSize)
        {
            var tasks = ReadTaskFromFile();

            var result = tasks.Skip((page - 1) * pageSize).Take(pageSize);
            return (result, tasks.Count);

        }

        public TaskDto GetTaskById(int id)
        {
            var tasks = ReadTaskFromFile();
            return tasks.FirstOrDefault(c => c.Id == id);
        }

        public void AddTask(TaskDto task)
        {
            var tasks = ReadTaskFromFile();
            task.Id = tasks.Count > 0 ? tasks.Max(c => c.Id) + 1 : 1;
            task.CreatedAt = DateTime.UtcNow;

            
            tasks.Add(task);
            WriteTasksToFile(tasks);
        
        }

        public TaskDto UpdateTask(TaskDto updatedTask)
        {
            var tasks = ReadTaskFromFile();

            var task = tasks.FirstOrDefault(c => c.Id == updatedTask.Id);
            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.IsCompleted = updatedTask.IsCompleted;
                task.UpdatedAt = DateTime.UtcNow;
                WriteTasksToFile(tasks);
            }
            return task;
        }

        public TaskDto DeleteTask(int id)
        {
            var tasks = ReadTaskFromFile();

            var taskToRemove = tasks.FirstOrDefault(c => c.Id == id);
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
                WriteTasksToFile(tasks);
            }

            return taskToRemove;
        }
        public List<TaskDto> ReadTaskFromFile()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                var options = new JsonSerializerOptions
                {
                    Converters = { new NullableDateTimeConverter() }
                };
                return JsonSerializer.Deserialize<List<TaskDto>>(jsonData, options) ?? new List<TaskDto>();
            }
            return new List<TaskDto>();
        }

        private void WriteTasksToFile(List<TaskDto> tasks)
        {
            var jsonData = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }


    }
}
