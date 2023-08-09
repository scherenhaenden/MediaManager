using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
using BeOneSender.Data.Services.Database.Artist;
using Microsoft.Data.Sqlite;

namespace BeOneSender.Data.Tests.Services.Artists;

[TestFixture]
public class ArtistDataServiceTests
{
    // Mocked DbContext and DbSet for testing
    private DbConnection _connection;
    private DbContextOptions<BeOneSenderDataContext> _options;


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

    [Test]
    public async Task AddArtistAsync_Should_Add_Artist_And_Return_DataModel()
    {
        // Arrange
        using (var context = new BeOneSenderDataContext(_options))
        {
            var artistDataService = new ArtistDataService(context);
            var artistName = "TestArtist";

            // Act
            var result = await artistDataService.AddArtistAsync(artistName);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(artistName, result.Name);

            // Ensure that the artist was actually added to the database
            var addedArtist = await context.Artists.FirstOrDefaultAsync(a => a.Name == artistName);
            Assert.NotNull(addedArtist);
            Assert.AreEqual(artistName, addedArtist.Name);
        }
    }
    // Similar test methods for other public methods in ArtistDataService...
}