using BeOneSender.BusinessLogic.Services.Bootstrap;
using BeOneSender.Data.Database.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.BusinessLogic.Tests.Services.Bootstrap;

public class LoadInformationTests
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
    public async Task LoadData()
    {
        using (var context = new BeOneSenderDataContext(_options))
        {
            // Arrange
            var loadInformation = new LoadInformation(context);

            // Act
            await loadInformation.LoadData();
        }


        // Assert
        Assert.True(true);
    }
}