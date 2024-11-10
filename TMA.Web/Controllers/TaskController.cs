using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TMA.Api.Repository;
using TMA.Model;

namespace TMA.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Retrieves a paginated list of tasks.
        /// </summary>
        /// <param name="page">The page number to retrieve. Default is 1.</param>
        /// <param name="pageSize">The number of tasks per page. Default is 10.</param>
        /// <returns>A list of tasks with pagination.</returns>
        
        [HttpGet("GetTasks")]
        [SwaggerOperation(Summary = "Get Task List", Description = "", OperationId = "GetTaskList", Tags = new[] { "Task" })]
        public async Task<IActionResult> GetTaskList(int page = 1, int pageSize = 10)
        {
            var (tasks, count) = await _taskRepository.GetAllAsync(page, pageSize);

            return Ok(new
            {
                Message = "Tasks retrieved successfully",
                Tasks = tasks,
                TotalRecords = count
            });
        }
        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="TaskId">The ID of the task to retrieve.</param>
        /// <returns>The requested task.</returns>
        
        [HttpGet("GetTaskById")]
        [SwaggerOperation(Summary = "Get Task By Id", Description = "", OperationId = "GetTaskById", Tags = new[] { "Task" })]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
   
            if (task == null)
            {
                return NotFound(new { Message = $"Task with ID {taskId} not found." });
            }

            return Ok(task);
        }
        /// <summary>
        /// Adds a new task.
        /// </summary>
        /// <param name="newTask">The task to add.</param>
        /// <returns>The created task.</returns>
        
        [HttpPost("AddTask")]
        [SwaggerOperation(Summary = "Add Task", Description = "", OperationId = "AddTask", Tags = new[] { "Task" })]
        public async Task<IActionResult> AddTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest(new { Message = "Invalid task data" });
            }
            var createdTask = await _taskRepository.CreateAsync(taskDto);

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.TaskId },
                new { Message = "Task added successfully", Task = createdTask });
        }
        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="updatedTask">The task data to update.</param>
        /// <returns>The updated task.</returns>
        
        [HttpPut("UpdateTask")]
        [SwaggerOperation(Summary = "Update Task", Description = "", OperationId = "UpdateTask", Tags = new[] { "Task" })]
        public async Task<IActionResult> UpdateTask([FromBody] TaskDto taskDto)
        {
            if (taskDto.TaskId == 0)
            {
                return BadRequest(new { Message = "Invalid Task ID" });
            }

            var updatedTask = await _taskRepository.UpdateAsync(taskDto);

            if (updatedTask == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            return Ok(new { Message = "Task updated successfully", UpdatedTask = updatedTask });
        }
        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A confirmation message.</returns>

        [HttpDelete("DeleteTask")]
        [SwaggerOperation(Summary = "Delete Task", Description = "", OperationId = "DeleteTask", Tags = new[] { "Task" })]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deletedTask = await _taskRepository.DeleteAsync(id);

            if (deletedTask == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            return Ok(new { Message = "Task deleted successfully", DeletedTaskId = id });
        }

      
    }
}

