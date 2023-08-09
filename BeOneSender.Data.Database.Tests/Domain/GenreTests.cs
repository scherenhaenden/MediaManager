using System.ComponentModel.DataAnnotations;
using BeOneSender.Data.Database.Domain;

namespace BeOneSender.Data.Database.Tests.Domain;

[TestFixture]
public class GenreTests
{
    [Test]
    public void Genre_Id_ShouldBeRequired()
    {
        // Arrange
        var genre = new GenreDatabaseModel { Name = "Pop", Description = "Popular Music" };

        // Act
        var context = new ValidationContext(genre, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(genre, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Id' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Id")),
            "The 'Id' property should have validation errors.");
    }

    [Test]
    public void Genre_Name_ShouldBeRequired()
    {
        // Arrange
        var genre = new GenreDatabaseModel { Id = Guid.NewGuid(), Description = "Popular Music" };

        // Act
        var context = new ValidationContext(genre, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(genre, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Name' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Name")),
            "The 'Name' property should have validation errors.");
    }

    [Test]
    public void Genre_Name_ShouldNotExceedMaxLength()
    {
        // Arrange
        var genre = new GenreDatabaseModel
        {
            Id = Guid.NewGuid(),
            Name = new string('A', 101),
            Description = "Popular Music"
        };

        // Act
        var context = new ValidationContext(genre, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(genre, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Name' property should not exceed 100 characters in length.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Name")),
            "The 'Name' property should have validation errors.");
    }
    
    [Test]
    public void Adding_Song_Should_Fill_SongCollection_In_Genre()
    {
        // Arrange
        var genre = new GenreDatabaseModel { Name = "Pop", Description = "Popular Music" };
        var song = new SongDatabaseModel { Title = "Song Title", Genre = genre };

        // Act
        genre.SongDatabaseModel.Add(song);

        // Assert
        Assert.AreEqual(1, genre.SongDatabaseModel.Count,
            "The 'SongDatabaseModel' collection in the genre should have one song.");
        Assert.AreSame(song, genre.SongDatabaseModel.First(),
            "The added song should be the same as the one in the 'SongDatabaseModel' collection.");
    }
}