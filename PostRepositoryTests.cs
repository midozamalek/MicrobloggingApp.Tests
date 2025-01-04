using MicrobloggingApp.Domain;
using MicrobloggingApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PostRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly PostRepository _postRepository;

    public PostRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureDeleted(); // Clear the database before each test
        _postRepository = new PostRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPosts()
    {
        // Arrange
        var posts = new List<Post>
        {
            new Post { Id = 1, UserId = "user123", Text = "Test post 1", OriginalImageUrl = "http://example.com/image1.jpg", Latitude = 0.0, Longitude = 0.0, CreatedAt = DateTime.UtcNow },
            new Post { Id = 2, UserId = "user456", Text = "Test post 2", OriginalImageUrl = "http://example.com/image2.jpg", Latitude = 0.0, Longitude = 0.0, CreatedAt = DateTime.UtcNow }
        };

        await _context.Posts.AddRangeAsync(posts);
        await _context.SaveChangesAsync();

        // Act
        var result = await _postRepository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Text == "Test post 1");
        Assert.Contains(result, p => p.Text == "Test post 2");
    }

    [Fact]
    public async Task AddAsync_ShouldAddPost()
    {
        // Arrange
        var post = new Post
        {
            Id = 1000,
            UserId = "user123",
            Text = "Test post",
            OriginalImageUrl = "http://example.com/image.jpg",
            Latitude = 0.0,
            Longitude = 0.0,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        await _postRepository.AddAsync(post);

        // Assert
        var addedPost = await _context.Posts.FindAsync(post.Id);
        Assert.NotNull(addedPost);
        Assert.Equal(post.Text, addedPost.Text);
    }
}
