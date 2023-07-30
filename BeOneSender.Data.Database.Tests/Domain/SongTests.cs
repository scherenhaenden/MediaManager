using System.ComponentModel.DataAnnotations;
using BeOneSender.Data.Database.Domain;

namespace BeOneSender.Data.Database.Tests.Domain;

[TestFixture]
public class SongTests
{
    [Test]
    public void Song_Title_ShouldBeRequired()
    {
        // Arrange
        var song = new SongDatabaseModel { Path = "song.mp3", ArtistId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(song, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(song, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Title' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Title")),
            "The 'Title' property should have validation errors.");
    }

    [Test]
    public void Song_Title_ShouldNotExceedMaxLength()
    {
        // Arrange
        var song = new SongDatabaseModel
            { Title = new string('A', 501), Path = "song.mp3", ArtistId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(song, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(song, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Title' property should not exceed 500 characters in length.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Title")),
            "The 'Title' property should have validation errors.");
    }

    [Test]
    public void Song_Path_ShouldBeRequired()
    {
        // Arrange
        var song = new SongDatabaseModel { Title = "Song Title", ArtistId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(song, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(song, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Path' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Path")),
            "The 'Path' property should have validation errors.");
    }

    [Test]
    public void Song_Path_ShouldNotExceedMaxLength()
    {
        // Arrange
        var song = new SongDatabaseModel
            { Title = "Song Title", Path = new string('A', 501), ArtistId = Guid.NewGuid(), GenreId = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(song, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(song, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Path' property should not exceed 500 characters in length.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Path")),
            "The 'Path' property should have validation errors.");
    }

    [Test]
    public void Song_ArtistId_ShouldBeRequired()
    {
        // Arrange
        var song = new SongDatabaseModel { Title = "Song Title", Path = "song.mp3", GenreId = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(song, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(song, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'ArtistId' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("ArtistId")),
            "The 'ArtistId' property should have validation errors.");
    }

    [Test]
    public void Song_GenreId_ShouldBeRequired()
    {
        // Arrange
        var song = new SongDatabaseModel { Title = "Song Title", Path = "song.mp3", ArtistId = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(song, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(song, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'GenreId' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("GenreId")),
            "The 'GenreId' property should have validation errors.");
    }
}