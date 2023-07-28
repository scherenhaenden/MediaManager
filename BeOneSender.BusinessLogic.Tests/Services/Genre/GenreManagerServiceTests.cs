using BeOneSender.BusinessLogic.Services.SongLibraryManagement;
using BeOneSender.Data.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

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
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public async Task AddGenre_ShouldAddGenreAndSongToDatabase()
        {
            // Arrange
            using (var context = new BeOneSenderDataContext(_options))
            {
                var genreService = new GenreManagerService(context);

                // Act
                await genreService.AddGenre("Test Song", "Test Artist", "Test Genre", "test.mp3", CancellationToken.None);
            }

            // Assert
            using (var context = new BeOneSenderDataContext(_options))
            {
                // Check if the genre is added to the database
                var genre = context.Genres.FirstOrDefault(g => g.Name == "Test Genre");
                Assert.NotNull(genre, "Genre should be added to the database.");

                // Check if the artist is added to the database
                var artist = context.Artists.FirstOrDefault(a => a.Name == "Test Artist");
                Assert.NotNull(artist, "Artist should be added to the database.");

                // Check if the song is added to the database
                var song = context.Songs.FirstOrDefault(s => s.Title == "Test Song");
                Assert.NotNull(song, "Song should be added to the database.");

                // Check if the song is associated with the correct genre and artist
                Assert.AreEqual(genre.Id, song.GenreId, "GenreId should match the added genre.");
                Assert.AreEqual(artist.Id, song.ArtistId, "ArtistId should match the added artist.");
            }
        }

        // Add more test methods for other scenarios and edge cases as needed.
    }

public class TestFixtureAttribute : Attribute
{
}