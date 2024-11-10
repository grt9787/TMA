using Microsoft.EntityFrameworkCore;
using TMA.Api.Model;
using TMA.Model;

namespace TMA.Api.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<TaskDto> tasks, int count)> GetAllAsync(int page, int pageSize)
        {
            var entities = await _context.Tasks
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var tasks = entities.Select(x => new TaskDto
            {
                TaskId = x.TaskId,
                TaskTypeId = x.TaskTypeId,
                TaskStatusId = x.TaskStatusId,
                Title = x.Title,
                Description = x.Description,
                IsCompleted = x.IsCompleted,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate
            });

            var totalCount = await _context.Tasks.CountAsync();

            return (tasks, totalCount);
        }


        public async Task<TaskDto> GetByIdAsync(int taskId)
        {
            var entity = await _context.Tasks.FindAsync(taskId);
            if (entity == null)
            {
                return null;
            }

            var task = new TaskDto
            {
                TaskId = entity.TaskId,
                TaskTypeId = entity.TaskTypeId,
                TaskStatusId = entity.TaskStatusId,
                Title = entity.Title,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate
            };

            return task;
        }

        public async Task<TaskDto> CreateAsync(TaskDto taskDto)
        {
            var task = new Tasks
            {
                TaskTypeId = taskDto.TaskTypeId,
                TaskStatusId = taskDto.TaskStatusId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                IsCompleted = taskDto.IsCompleted,
                CreatedBy = taskDto.CreatedBy,
                CreatedDate = DateTime.Now
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            taskDto.TaskId = task.TaskId;
            taskDto.CreatedDate = task.CreatedDate;

            return taskDto;
        }

        public async Task<TaskDto> UpdateAsync(TaskDto taskDto)
        {
            var existingTask = await _context.Tasks.FindAsync(taskDto.TaskId);
            if (existingTask == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            existingTask.TaskTypeId = taskDto.TaskTypeId;
            existingTask.TaskStatusId = taskDto.TaskStatusId;
            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.IsCompleted = taskDto.IsCompleted;
            existingTask.ModifiedBy = taskDto.ModifiedBy;
            existingTask.ModifiedDate = DateTime.Now;

            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
            var task = new TaskDto
            {
                TaskId = existingTask.TaskId,
                TaskTypeId = existingTask.TaskTypeId,
                TaskStatusId = existingTask.TaskStatusId,
                Title = existingTask.Title,
                Description = existingTask.Description,
                IsCompleted = existingTask.IsCompleted,
                CreatedBy = existingTask.CreatedBy,
                CreatedDate = existingTask.CreatedDate,
                ModifiedBy = existingTask.ModifiedBy,
                ModifiedDate = existingTask.ModifiedDate
            };
            return task;
        }

        public async Task<TaskDto> DeleteAsync(int taskId)
        {
            var entity = await _context.Tasks.FindAsync(taskId);
            var task = new TaskDto
            {
                TaskId = entity.TaskId,
                TaskTypeId = entity.TaskTypeId,
                TaskStatusId = entity.TaskStatusId,
                Title = entity.Title,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate
            };

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
            return task;
        }
    }

   
}
