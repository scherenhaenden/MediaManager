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
}