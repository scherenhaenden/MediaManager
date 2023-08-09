using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Database.Tests.Core.Configuration;

//using System.Linq;

[TestFixture]
public class BeOneSenderDataContextTests
{
    [SetUp]
    public void SetUp()
    {
        // Use in-memory database for testing
        _options = new DbContextOptionsBuilder<BeOneSenderDataContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
    }

    [Test]
    public void Test_CreationOfContext_Using_Context()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            // Act
            var artists = context.Artists.ToList();
            var genres = context.Genres.ToList();
            var songs = context.Songs.ToList();

            // Assert
            Assert.NotNull(artists, "The 'Artists' property should not be null.");
            Assert.NotNull(genres, "The 'Genres' property should not be null.");
            Assert.NotNull(songs, "The 'Songs' property should not be null.");
        }
    }



    private DbContextOptions<BeOneSenderDataContext> _options;

    [Test]
    public void AddSong_ShouldAddSongToDatabase()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var song = new SongDatabaseModel
            {
                Title = "Test Song",
                Path = "test.mp3"
            };

            // Act
            context.Songs.Add(song);
            context.SaveChanges();
        }

        // Assert
        using (var context = new BeOneSenderDataContext(_options))
        {
            var song = context.Songs.FirstOrDefault(s => s.Title == "Test Song");
            Assert.NotNull(song, "Song should be added to the database.");
        }
    }


    [Test]
    public void GetSongsByCriteria_ShouldReturnMatchingSongs()
    {
        var artistId = Guid.NewGuid();
        var genreId = Guid.NewGuid();
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            context.Songs.AddRange(
                new SongDatabaseModel { Title = "Song 1", ArtistId = artistId, GenreId = genreId, Path = "song1.mp3" },
                new SongDatabaseModel
                    { Title = "Song 2", ArtistId = artistId, GenreId = Guid.NewGuid(), Path = "song2.mp3" },
                new SongDatabaseModel
                    { Title = "Song 3", ArtistId = Guid.NewGuid(), GenreId = genreId, Path = "song3.mp3" }
            );

            context.SaveChanges();
        }

        // Act
        using (var context = new BeOneSenderDataContext(_options))
        {
            var songs = context.Songs.ToList().Where(s => s.ArtistId == artistId && s.GenreId == genreId).ToList();

            // Assert
            Assert.AreEqual(1, songs.Count, "Should return one matching song.");
            Assert.AreEqual("Song 1", songs[0].Title, "The returned song title should match.");
        }
    }

    // Add more test methods for other functionalities as needed.
}