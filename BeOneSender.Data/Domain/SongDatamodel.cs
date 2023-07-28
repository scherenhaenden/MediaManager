using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeOneSender.Data.Domain;

[Table("Songs")]
public class Song
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]
    public Guid Id { get; set; }
    [Required]
    [StringLength(500)]
    public string Title { get; set; }
    [Required]
    [StringLength(500)]
    public string Path { get; set; }
    
    [RequiredGuid(ErrorMessage = "The 'ArtistId' property should be required.")]
    public Guid ArtistId { get; set; }
    [RequiredGuid(ErrorMessage = "The 'GenreId' property should be required.")]
    public Guid GenreId { get; set; }
}