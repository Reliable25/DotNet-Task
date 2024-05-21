using Dot_NET_Task.Controllers;
using Dot_NET_Task.Data;
using Dot_NET_Task.DTO;
using Dot_NET_Task.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace Dot_NET_Task.UnitTest
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _controller = new UserController(_mockUserRepository.Object);
        }
        [Fact]
            public async Task Create_Returns_BadRequest_If_PersonalInformation_Is_Invalid()
            {
                // Arrange
                var userRepositoryMock = new Mock<IUserRepository>();
                var controller = new UserController(userRepositoryMock.Object);
                var request = new CreateUserRequest { PersonalInformation = new PersonalInformation() };

                // Act
                var result = await controller.Create(request);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal("FirstName, LastName, and Email are mandatory fields.", badRequestResult.Value);
            }
        [Fact]
        public async Task Create_Returns_BadRequest_If_Phone_Is_Not_Numeric()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                PersonalInformation = new PersonalInformation
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "ABC12345678"
                }
            };

            // Act
            var result = await _controller.Create(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Phone should only contain numeric characters", badRequestResult.Value);
        }

        [Fact]
        public async Task Create_Returns_Ok_When_Request_Is_Valid()
        {
            // Arrange
            var personalInfo = new PersonalInformation
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "12345678901"
            };

            var request = new CreateUserRequest
            {
                PersonalInformation = personalInfo,
                YesNoAnswer = true,
                ParagraphAnswer = "Test paragraph",
                NumericAnswer = 123,
                DropdownOptions = new string[] { "Option1", "Option2" }, // Change to array
                DropdownAnswer = "Option1",
                MaxChoiceAllowed = 2
            };

            _mockUserRepository.Setup(repo => repo.CreateUserAsync(It.IsAny<PersonalInformation>()))
                .ReturnsAsync(personalInfo);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<CreateUserResponse>(okResult.Value);

            Assert.Equal(personalInfo, response.PersonalInformation);
            Assert.Single(response.YesNoQuestions);
            Assert.Single(response.DateQuestions);
            Assert.Single(response.ParagraphQuestions);
            Assert.Single(response.NumericQuestions);
            Assert.Single(response.DropdownQuestions);
            Assert.Single(response.MultipleChoiceQuestions);
        }
    }

}

