using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeOneSender.Data.Domain;

[Table("Artists")]
public class Artist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]
    public Guid Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Name { get; set; }

    [RequiredGuid(ErrorMessage = "The 'GenreId' property should be required.")]
    public Guid GenreId { get; set; }
}