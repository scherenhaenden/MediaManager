using System.ComponentModel.DataAnnotations;
using SharedTools.Validation.Attributes;

namespace BeOneSender.Data.Models;

public class GenreDataModel
{
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]

    public Guid Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    public string? Description { get; set; }
}