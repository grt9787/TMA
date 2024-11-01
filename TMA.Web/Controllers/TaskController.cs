using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TMA.DAL;
using TMA.Model;

namespace TMA.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly string _filePath = "tasks.json";
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _TaskRepository;

        public TaskController(ILogger<TaskController> logger, ITaskRepository TaskRepository)
        {
            _logger = logger;
            _TaskRepository = TaskRepository;
        }

        /// <summary>
        /// Retrieves a paginated list of tasks.
        /// </summary>
        /// <param name="page">The page number to retrieve. Default is 1.</param>
        /// <param name="pageSize">The number of tasks per page. Default is 10.</param>
        /// <returns>A list of tasks with pagination.</returns>
        
        [HttpGet("GetTasks")]
        [SwaggerOperation(Summary = "Get Task List", Description = "", OperationId = "GetTaskList", Tags = new[] { "Task" })]
        public IActionResult GetTasks(int page = 1, int pageSize = 10)
        {
            var result = _TaskRepository.GetAllTasks(page, pageSize);

            return Ok(new
            {
                Message = "Tasks retrieved successfully",
                Tasks = result.tasks,
                TotalRecords = result.count
            });
        }
        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="TaskId">The ID of the task to retrieve.</param>
        /// <returns>The requested task.</returns>
        
        [HttpGet("GetTaskById")]
        [SwaggerOperation(Summary = "Get Task By Id", Description = "", OperationId = "GetTaskById", Tags = new[] { "Task" })]
        public IActionResult GetTaskById(int TaskId)
        {
            var Tasks = new List<TaskDto>();

            var Task = _TaskRepository.GetTaskById(TaskId);

            if (Task == null)
            {
                return NotFound(new { Message = $"Task with ID {TaskId} not found." });
            }

            return Ok(Task);
        }
        /// <summary>
        /// Adds a new task.
        /// </summary>
        /// <param name="newTask">The task to add.</param>
        /// <returns>The created task.</returns>
        
        [HttpPost("AddTask")]
        [SwaggerOperation(Summary = "Add Task", Description = "", OperationId = "AddTask", Tags = new[] { "Task" })]
        public IActionResult AddTask([FromBody] TaskDto newTask)
        {
          _TaskRepository.AddTask(newTask);

            var tasks  = new List<TaskDto>();

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id },
                new { Message = "Task added successfully", Task = newTask });
        }
        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="updatedTask">The task data to update.</param>
        /// <returns>The updated task.</returns>
        
        [HttpPut("UpdateTask")]
        [SwaggerOperation(Summary = "Update Task", Description = "", OperationId = "UpdateTask", Tags = new[] { "Task" })]
        public IActionResult UpdateTask([FromBody] TaskDto updatedTask)
        {
            //updatedTask.Id = updatedTask.Id;
            var Task = _TaskRepository.UpdateTask(updatedTask);

            if (Task == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            return Ok(new { Message = "Task updated successfully", UpdatedTask = Task });
        }
        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A confirmation message.</returns>

        [HttpDelete("DeleteTask")]
        [SwaggerOperation(Summary = "Delete Task", Description = "", OperationId = "DeleteTask", Tags = new[] { "Task" })]
        public IActionResult DeleteTask(int id)
        {
            var Task = _TaskRepository.DeleteTask(id);
            if (Task == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            return Ok(new { Message = "Task deleted successfully", DeletedTaskId = id });
        }


    }
}

