using System.Data.Common;
using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Services.Database.Genre;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Tests.Services.Genre;

[TestFixture]
public class GenreDataServiceTests
{
    /*[SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<BeOneSenderDataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }*/

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        _options = new DbContextOptionsBuilder<BeOneSenderDataContext>()
            .UseSqlite(_connection)
            .Options;

        using (var context = new BeOneSenderDataContext(_options))
        {
            context.Database.EnsureCreated();
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _connection.Dispose();
    }

    private DbConnection _connection;
    private DbContextOptions<BeOneSenderDataContext> _options;

    [Test]
    public async Task AddGenre_ShouldAddGenreToDatabase()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreDataService = new GenreDataService(context);

            // Act
            var genreDataModel =
                await genreDataService.AddGenre("Test Genre", "Test Description", CancellationToken.None);

            // Assert
            Assert.NotNull(genreDataModel, "Genre should be added to the database.");
            Assert.AreEqual("Test Genre", genreDataModel.Name, "Genre name should match the added genre.");
        }
    }

    [Test]
    public async Task AddGenre_ShouldThrowExceptionIfGenreNameIsNotUnique()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreDataService = new GenreDataService(context);
            await genreDataService.AddGenre("Test Genre", "Test Description",
                CancellationToken.None); // Add the genre first

            // Act & Assert
            Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await genreDataService.AddGenre("Test Genre", "Test Description",
                    CancellationToken.None); // Attempt to add duplicate genre
            });
        }
    }

    [Test]
    public async Task GetGenre_ShouldReturnGenreIfExists()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreDataService = new GenreDataService(context);
            await genreDataService.AddGenre("Test Genre", "Test Description",
                CancellationToken.None); // Add the genre first

            // Act
            var genreDataModel = await genreDataService.GetGenreAsync("Test Genre", CancellationToken.None);

            // Assert
            Assert.NotNull(genreDataModel, "Genre should exist in the database.");
            Assert.AreEqual("Test Genre", genreDataModel.Name, "Genre name should match the queried genre.");
        }
    }

    [Test]
    public async Task GetGenre_ShouldReturnNullIfGenreDoesNotExist()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreDataService = new GenreDataService(context);

            // Act
            var genreDataModel = await genreDataService.GetGenreAsync("Test Genre", CancellationToken.None);

            // Assert
            Assert.Null(genreDataModel, "Genre should not exist in the database.");
        }
    }

    [Test]
    public async Task AddOrGetGenre_ShouldAddGenreIfNotExists()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreDataService = new GenreDataService(context);

            // Act
            var genreDataModel =
                await genreDataService.AddOrGetGenre("Test Genre", "Test Description", CancellationToken.None);

            // Assert
            Assert.NotNull(genreDataModel, "Genre should be added to the database.");
            Assert.AreEqual("Test Genre", genreDataModel.Name, "Genre name should match the added genre.");
        }
    }

    [Test]
    public async Task AddOrGetGenre_ShouldGetGenreIfExists()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var genreDataService = new GenreDataService(context);
            await genreDataService.AddGenre("Test Genre", "Test Description",
                CancellationToken.None); // Add the genre first

            // Act
            var genreDataModel =
                await genreDataService.AddOrGetGenre("Test Genre", "Test Description", CancellationToken.None);

            // Assert
            Assert.NotNull(genreDataModel, "Genre should exist in the database.");
            Assert.AreEqual("Test Genre", genreDataModel.Name, "Genre name should match the queried genre.");
        }
    }
}