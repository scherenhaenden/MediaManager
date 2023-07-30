using System.ComponentModel.DataAnnotations;
using BeOneSender.Data.Database.Domain;

namespace BeOneSender.Data.Database.Tests.Domain;

[TestFixture]
public class ArtistTests
{
    [Test]
    public void Artist_Id_ShouldBeRequired()
    {
        // Arrange with bogus data
        //Bogus.


        // Arrange
        var artist = new ArtistDatabaseModel { Id = Guid.Empty, Name = "John Doe" };

        // Act
        var context = new ValidationContext(artist, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(artist, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Id' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Id")),
            "The 'Id' property should have validation errors.");
    }


    [Test]
    public void Artist_Name_ShouldBeRequired()
    {
        // Arrange
        var artist = new ArtistDatabaseModel { Id = Guid.NewGuid() };

        // Act
        var context = new ValidationContext(artist, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(artist, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Name' property should be required.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Name")),
            "The 'Name' property should have validation errors.");
    }

    [Test]
    public void Artist_Name_ShouldNotExceedMaxLength()
    {
        // Arrange
        var artist = new ArtistDatabaseModel { Id = Guid.NewGuid(), Name = new string('A', 501) };

        // Act
        var context = new ValidationContext(artist, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(artist, context, results, true);

        // Assert
        Assert.IsFalse(isValid, "The 'Name' property should not exceed 500 characters in length.");
        Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Name")),
            "The 'Name' property should have validation errors.");
    }
}