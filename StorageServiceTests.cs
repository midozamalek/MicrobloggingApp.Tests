using MicrobloggingApp.Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

public class StorageServiceTests
{
    [Fact]
    public async Task UploadAsync_ShouldReturnFileUrl()
    {
        // Arrange
        var mockStorageService = new Mock<IStorageService>();
        var fileMock = new Mock<IFormFile>();
        var content = "Hello World from a Fake File";
        var fileName = "test.txt";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(content);
        writer.Flush();
        ms.Position = 0;
        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ => _.FileName).Returns(fileName);
        fileMock.Setup(_ => _.Length).Returns(ms.Length);

        var expectedUrl = "http://example.com/test.txt";
        mockStorageService.Setup(s => s.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(expectedUrl);

        // Act
        var result = await mockStorageService.Object.UploadAsync(fileMock.Object);

        // Assert
        Assert.Equal(expectedUrl, result);
        mockStorageService.Verify(s => s.UploadAsync(It.IsAny<IFormFile>()), Times.Once);
    }
}
