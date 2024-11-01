using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TMA.Controllers;
using TMA.DAL;
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
        public void GetTasks_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<TaskDto>
            {
                new TaskDto { Id = 1, Title = "Task 1", Description = "Description 1" ,IsCompleted=false,CreatedAt=new DateTime(),UpdatedAt=null},
                new TaskDto { Id = 2, Title = "Task 2", Description = "Description 2" ,IsCompleted=false,CreatedAt=new DateTime(),UpdatedAt=null},
            };

            var response = (tasks, 2);

            _mockTaskRepository.Setup(repo => repo.GetAllTasks(It.IsAny<int>(), It.IsAny<int>())).Returns(response);

            // Act
            IActionResult responseResult = _controller.GetTasks();

            // Assert
            var okResult = (OkObjectResult)responseResult;
            var value = (dynamic)okResult.Value; // Cast to dynamic for easier access to properties
            Assert.AreNotEqual(value, null);

        }

        [Test]
        public void GetTaskById_ExistingId_ReturnsOkResult_WithTask()
        {
            // Arrange
            var task = new TaskDto { Id = 1, Title = "Task 1", Description = "Description 1", IsCompleted = false, CreatedAt = new DateTime(), UpdatedAt = null };
            _mockTaskRepository.Setup(repo => repo.GetTaskById(1))
                               .Returns(task);

            // Act
            IActionResult result = _controller.GetTaskById(1);

            // Assert
            var okResult = result as OkObjectResult;
            var returnedTask = okResult.Value as TaskDto;
            Assert.AreEqual(task.Id, returnedTask.Id);
            Assert.AreEqual(task.Title, returnedTask.Title);
            Assert.AreEqual(task.Description, returnedTask.Description);
            Assert.AreEqual(task.IsCompleted, returnedTask.IsCompleted);
            Assert.AreEqual(task.CreatedAt, returnedTask.CreatedAt);
            Assert.AreEqual(task.UpdatedAt, returnedTask.UpdatedAt);
        }

        [Test]
        public void GetTaskById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.GetTaskById(99))
                               .Returns((TaskDto)null);

            // Act
            IActionResult result = _controller.GetTaskById(99);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void AddTask_ValidTask_ReturnsCreatedAtAction()
        {
            // Arrange
            var newTask = new TaskDto
            {
                Title = "New Task",
                Description = "Description",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            //var tasks = new List<TaskDto>();

            //_mockTaskRepository.Setup(repo => repo.ReadTaskFromFile()).Returns(tasks);
            _mockTaskRepository.Setup(repo => repo.AddTask(newTask)).Verifiable(); ;

            // Act
            IActionResult result = _controller.AddTask(newTask);
            // Assert
            var createdAtActionResult = (CreatedAtActionResult)result;
            var value = (dynamic)createdAtActionResult.Value;
            Assert.AreNotEqual(newTask.Id, null);

        }
        [Test]
        public void UpdateTask_ExistingTask_ReturnsOkResult()
        {
            // Arrange
            var updatedTask = new TaskDto { Id = 1, Title = "Updated Task", Description = "Updated Description" };
            _mockTaskRepository.Setup(repo => repo.UpdateTask(updatedTask)).Returns(updatedTask);

            // Act
            IActionResult result = _controller.UpdateTask(updatedTask);

            // Assert
            var okResult = (OkObjectResult)result;
            var value = (dynamic)okResult.Value;
            Assert.AreNotEqual(value, null);
        }

        [Test]
        public void UpdateTask_NonExistingTask_ReturnsNotFound()
        {
            // Arrange
            var updatedTask = new TaskDto { Id = 99, Title = "Non-existing Task", Description = "No Description" };
            _mockTaskRepository.Setup(repo => repo.UpdateTask(updatedTask)).Returns((TaskDto)null);

            // Act
            IActionResult result = _controller.UpdateTask(updatedTask);

            // Assert
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreNotEqual(notFoundResult, null);
        }

        [Test]
        public void DeleteTask_ExistingId_ReturnsOkResult()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.DeleteTask(1)).Returns(new TaskDto { Id = 1 });

            // Act
            IActionResult result = _controller.DeleteTask(1);


            // Assert
            var okResult = (OkObjectResult)result;
            Assert.AreNotEqual(okResult, null);
        }

        [Test]
        public void DeleteTask_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.DeleteTask(99)).Returns((TaskDto)null);

            // Act
            IActionResult result = _controller.DeleteTask(99);

            // Assert
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreNotEqual(notFoundResult, null);
        }
    }
}
