using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MicrobloggingApp.Applications;
using MicrobloggingApp.Domain;
using MicrobloggingApp.DTO;
using MicrobloggingApp.Infrastructure.Storage;
using MicrobloggingApp.Queries;
using Moq;
using Xunit;

public class GetTimelineQueryHandlerTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IStorageService> _storageServiceMock;
    private readonly GetTimelineQueryHandler _handler;

    public GetTimelineQueryHandlerTests()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _storageServiceMock = new Mock<IStorageService>();
        _handler = new GetTimelineQueryHandler(_postRepositoryMock.Object, _storageServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsTimelineDtos()
    {
        // Arrange
        var posts = new List<Post>
        {
            new Post { Id = 1, Text = "Post 1", OriginalImageUrl = "http://example.com/image1.jpg", Latitude = 10.0, Longitude = 20.0, CreatedAt = DateTime.UtcNow },
            new Post { Id = 2, Text = "Post 2", OriginalImageUrl = "http://example.com/image2.jpg", Latitude = 30.0, Longitude = 40.0, CreatedAt = DateTime.UtcNow }
        };
        _postRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(posts);

        // Act
        var result = await _handler.Handle(new GetTimelineQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Post 1", result[0].Text);
        Assert.Equal("Post 2", result[1].Text);
    }


    [Fact]
    public void DetermineImageSize_ReturnsOriginalImageUrlForLargeScreen()
    {
        // Arrange
        var post = new Post { OriginalImageUrl = "http://example.com/image.jpg" };
        var handler = new GetTimelineQueryHandler(_postRepositoryMock.Object, _storageServiceMock.Object);

        // Act
        var result = handler.DetermineImageSize(post);

        // Assert
        Assert.Equal("http://example.com/image.jpg", result);
    }

    [Fact]
    public void GetResizedImageUrl_ReturnsCorrectUrl()
    {
        // Arrange
        var handler = new GetTimelineQueryHandler(_postRepositoryMock.Object, _storageServiceMock.Object);
        var imageUrl = "http://example.com/image.jpg";
        var width = "300,200";

        // Act
        var result = handler.GetResizedImageUrl(imageUrl, width);

        // Assert
        Assert.Contains("resize=300,200", result);
    }
}
