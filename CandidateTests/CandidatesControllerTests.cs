using CandidateCoreService.Controllers;
using CandidateCoreService.Model;
using CandidateCoreService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;

namespace YourNamespace.Tests
{
    [TestFixture]  // NUnit test class attribute
    public class CandidatesControllerTests
    {
        private Mock<ICandidateRepository> _repositoryMock;
        private CandidatesController _controller;

        // Setup method to initialize mock repository and controller
        [SetUp]  // NUnit setup method
        public void Setup()
        {
            _repositoryMock = new Mock<ICandidateRepository>();
            _controller = new CandidatesController(_repositoryMock.Object);
        }

        [Test]  // NUnit test method attribute
        public async Task AddOrUpdateCandidate_ShouldCreate_WhenNew()
        {
            // Arrange: create a new candidate DTO
            var dto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "New Candidate"
            };

            // Setup mock repository to return null (meaning no existing candidate found)
            _repositoryMock.Setup(repo => repo.GetByEmailAsync(dto.Email)).ReturnsAsync((Candidate)null);

            // Act: call the controller method
            var result = await _controller.AddOrUpdateCandidate(dto);

            // Assert: check that the result is a CreatedAtActionResult
            Assert.IsInstanceOf<CreatedAtActionResult>(result); // Assert using NUnit's method
        }

        // Add more tests for update, bad requests, etc.
    }
}
