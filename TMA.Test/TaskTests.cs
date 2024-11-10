using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TMA.Api.Repository;
using TMA.Controllers;
using TMA.Model;

namespace TMA.Api.Tests.Controllers
{
    [TestFixture]
    public class TaskControllerTests
    {
        private Mock<ITaskRepository> _mockTaskRepository;
        private Mock<ILogger<TaskController>> _mockLogger;
        private TaskController _controller;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _mockLogger = new Mock<ILogger<TaskController>>();
            _controller = new TaskController(_mockLogger.Object, _mockTaskRepository.Object);
        }
        [Test]
        public async Task GetTasks_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<TaskDto>
    {
                new TaskDto
    {
        TaskId = 1,
        TaskTypeId = 1,
        TaskStatusId = 1,
        Title = "Complete a Task",
        Description = "This is a sample task to be completed.",
        IsCompleted = true,
        CreatedBy = "John Doe",
        CreatedDate = new DateTime(),
        ModifiedBy = "Jane Smith",
        ModifiedDate = new DateTime()
    },
    new TaskDto
    {
        TaskId = 2,
        TaskTypeId = 2,
        TaskStatusId = 2,
        Title = "Pending Task",
        Description = "This task is still pending.",
        IsCompleted = false,
        CreatedBy = "Alice Johnson",
        CreatedDate = new DateTime(),
          ModifiedBy = "Jane Smith",
        ModifiedDate = new DateTime()
    },
    };


            var response = (tasks, 2);

            _mockTaskRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(response);
            // Act
            IActionResult responseResult = await _controller.GetTaskList();
            // Assert
            var okResult = (OkObjectResult)responseResult;
            var value = (dynamic)okResult.Value;
            Assert.AreNotEqual(value, null);
        }

        [Test]
        public async Task GetTaskById_ExistingId_ReturnsOkResult_WithTask()
        {
            // Arrange
            var task = new TaskDto { TaskId = 1, Title = "Task 1", Description = "Description 1", IsCompleted = false, CreatedDate = DateTime.Now };
            _mockTaskRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(task);

            // Act
            IActionResult result = await _controller.GetTaskById(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedTask = okResult.Value as TaskDto;
            Assert.AreEqual(task.TaskId, returnedTask.TaskId);
            Assert.AreEqual(task.Title, returnedTask.Title);
        }

        [Test]
        public async Task GetTaskById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((TaskDto)null);

            // Act
            IActionResult result = await _controller.GetTaskById(99);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task AddTask_ValidTask_ReturnsCreatedAtAction()
        {
            // Arrange
            var newTask = new TaskDto
            {
                Title = "New Task",
                Description = "Description",
                IsCompleted = false,
                CreatedDate = DateTime.UtcNow
            };

            _mockTaskRepository.Setup(repo => repo.CreateAsync(It.IsAny<TaskDto>())).ReturnsAsync(newTask);

            // Act
            IActionResult result = await _controller.AddTask(newTask);

            // Assert
            var createdAtActionResult = (CreatedAtActionResult)result;
            var value = (dynamic)createdAtActionResult.Value;
            Assert.AreNotEqual(newTask.TaskId, null);
        }

        [Test]
        public async Task UpdateTask_ExistingTask_ReturnsOkResult()
        {
            // Arrange
            var updatedTask = new TaskDto { TaskId = 1, Title = "Updated Task", Description = "Updated Description" };
            _mockTaskRepository.Setup(repo => repo.UpdateAsync(updatedTask)).Returns(Task.FromResult(updatedTask));

            // Act
            IActionResult result = await _controller.UpdateTask(updatedTask);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [Test]
        public async Task UpdateTask_NonExistingTask_ReturnsNotFound()
        {
            // Arrange
            var updatedTask = new TaskDto

            {
                TaskId = 99,
                TaskTypeId = 0,
                TaskStatusId = 0,
                Title = "Non-existing Task",
                Description = "No Description",
                IsCompleted = true,
                CreatedBy = "John Doe",
                CreatedDate = new DateTime(2023, 11, 6),
                ModifiedBy = "Jane Smith",
                ModifiedDate = null
            };


            _mockTaskRepository.Setup(repo => repo.UpdateAsync(updatedTask)).ReturnsAsync((TaskDto)null);

            // Act
            IActionResult result = await _controller.UpdateTask(updatedTask);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result, "Expected NotFoundObjectResult when updating a non-existing task.");
        }

        [Test]
        public async Task DeleteTask_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var taskToDelete = new TaskDto { TaskId = 1 };
            _mockTaskRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.FromResult(taskToDelete));

            // Act
            IActionResult result = await _controller.DeleteTask(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [Test]
        public async Task DeleteTask_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.DeleteAsync(99)).ReturnsAsync((TaskDto)null);

            // Act
            IActionResult result = await _controller.DeleteTask(99);

            // Assert

            Assert.IsInstanceOf<NotFoundObjectResult>(result, "Expected NotFoundObjectResult when deleting a non-existing task.");
        }

    }
}
