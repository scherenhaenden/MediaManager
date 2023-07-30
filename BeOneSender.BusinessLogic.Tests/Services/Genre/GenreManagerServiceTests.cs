using BeOneSender.BusinessLogic.Services.Genre;
using BeOneSender.Data.Database.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.BusinessLogic.Tests.Services.Genre;

[TestFixture]
public class GenreManagerServiceTests
{
    private DbContextOptions<BeOneSenderDataContext> _options;

    [SetUp]
    public void SetUp()
    {
        // Use in-memory database for testing
        _options = new DbContextOptionsBuilder<BeOneSenderDataContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
    }

    [Test]
    public async Task AddGenre_ShouldAddGenreToDatabase()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreService = new GenreManagerService(context);

            // Act
            await genreService.AddGenre("Test Genre", "Test description", CancellationToken.None);
        }

        // Assert
        using (var context = new BeOneSenderDataContext(_options))
        {
            // Check if the genre is added to the database
            var genre = context.Genres.FirstOrDefault(g => g.Name == "Test Genre");
            Assert.NotNull(genre, "Genre should be added to the database.");
        }
    }

    // Add more test methods for other scenarios and edge cases as needed.
}

public class TestFixtureAttribute : Attribute
{
}