using Dot_NET_Task.Controllers;
using Dot_NET_Task.Data;
using Dot_NET_Task.DTO;
using Dot_NET_Task.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Dot_NET_Task.UnitTest
{
    public class TypeControllerTests
    {
        private readonly Mock<ITypeRepository> _mockTypeRepository;
        private readonly TypeController _controller;

        public TypeControllerTests()
        {
            _mockTypeRepository = new Mock<ITypeRepository>();
            _controller = new TypeController(_mockTypeRepository.Object);
        }

        // Test for HTTP POST - CreateType
        [Fact]
        public async Task CreateType_Returns_Conflict_If_Type_Already_Exists()
        {
            // Arrange
            var existingType = new Question { Id = "1", Type = "ExistingType" };
            _mockTypeRepository.Setup(repo => repo.GetTypeAsync("ExistingType")).ReturnsAsync(existingType);

            var request = new QuestionDTO { Type = "ExistingType" };

            // Act
            var result = await _controller.CreateType(request);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.Equal("Type already exists.", conflictResult.Value);
        }

        [Fact]
        public async Task CreateType_Returns_Ok_If_Type_Is_Created()
        {
            // Arrange
            _mockTypeRepository.Setup(repo => repo.GetTypeAsync(It.IsAny<string>())).ReturnsAsync((Question)null);

            var newQuestion = new Question { Id = "2", Type = "NewType" };
            _mockTypeRepository.Setup(repo => repo.CreateTypeAsync(It.IsAny<Question>())).ReturnsAsync(newQuestion);

            var request = new QuestionDTO { Type = "NewType" };

            // Act
            var result = await _controller.CreateType(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var createdQuestion = Assert.IsType<Question>(okResult.Value);

            Assert.Equal("NewType", createdQuestion.Type);
        }
        // Test for HTTP PUT - UpdateType
        [Fact]
        public async Task UpdateType_Returns_NotFound_If_Type_Does_Not_Exist()
        {
            // Arrange
            _mockTypeRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Question)null);
            var type = new Question { Id = "1", Type = "UpdatedType" };

            // Act
            var result = await _controller.UpdateType(type);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateType_Returns_Ok_If_Type_Is_Updated()
        {
            // Arrange
            var existingType = new Question { Id = "1", Type = "ExistingType" };
            _mockTypeRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(existingType);

            var updatedType = new Question { Id = "1", Type = "UpdatedType" };
            _mockTypeRepository.Setup(repo => repo.UpdateTypeAsync(It.IsAny<Question>())).ReturnsAsync(updatedType);

            // Act
            var result = await _controller.UpdateType(updatedType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedType = Assert.IsType<Question>(okResult.Value);
            Assert.Equal("UpdatedType", returnedType.Type);
        }

        // Test for HTTP GET - GetType
        [Fact]
        public async Task GetType_Returns_NotFound_If_Type_Does_Not_Exist()
        {
            // Arrange
            _mockTypeRepository.Setup(repo => repo.GetTypeAsync(It.IsAny<string>())).ReturnsAsync((Question)null);

            // Act
            var result = await _controller.GetType("NonExistingType");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetType_Returns_Ok_If_Type_Exists()
        {
            // Arrange
            var existingType = new Question { Id = "1", Type = "ExistingType" };
            _mockTypeRepository.Setup(repo => repo.GetTypeAsync("ExistingType")).ReturnsAsync(existingType);

            // Act
            var result = await _controller.GetType("ExistingType");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedType = Assert.IsType<Question>(okResult.Value);
            Assert.Equal("ExistingType", returnedType.Type);
        }
    }
}

