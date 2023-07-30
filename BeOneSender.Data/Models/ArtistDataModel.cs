using System.ComponentModel.DataAnnotations;
using SharedTools.Validation.Attributes;

namespace BeOneSender.Data.Models;

public class ArtistDataModel
{
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]
    public Guid Id { get; set; }

    [Required] [StringLength(500)] public string Name { get; set; } = null!;

    [RequiredGuid(ErrorMessage = "The 'GenreId' property should be required.")]
    public Guid GenreId { get; set; }
}