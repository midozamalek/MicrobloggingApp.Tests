using System;
using System.Threading;
using System.Threading.Tasks;
using MicrobloggingApp.Applications;
using MicrobloggingApp.Domain;
using MicrobloggingApp.Infrastructure.Storage;
using MicrobloggingApp.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

public class CreatePostCommandHandlerTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IStorageService> _storageServiceMock;
    private readonly CreatePostCommandHandler _handler;

    public CreatePostCommandHandlerTests()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _storageServiceMock = new Mock<IStorageService>();
        _handler = new CreatePostCommandHandler(_postRepositoryMock.Object, _storageServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePost_WhenImageIsProvided()
    {
        // Arrange
        var command = new CreatePostCommand
        (
              "Test post",
              new Mock<IFormFile>().Object,
            "user123"
        );
        _storageServiceMock.Setup(s => s.UploadAsync(command.Image)).ReturnsAsync("http://example.com/image.jpg");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        _postRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreatePost_WhenImageIsNotProvided()
    {
        // Arrange
        var command = new CreatePostCommand
        (
              "Test post",
              null,
            "user123"
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        _postRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);
    }
}
