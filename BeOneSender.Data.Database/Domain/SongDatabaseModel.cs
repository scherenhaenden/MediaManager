using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedTools.Validation.Attributes;

namespace BeOneSender.Data.Database.Domain;

[Table("Songs")]
public class SongDatabaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] [StringLength(500)] public string Title { get; set; } = null!;

    [Required] [StringLength(500)] public string Path { get; set; } = null!;

    [ForeignKey("Artists")]
    [RequiredGuid(ErrorMessage = "The 'ArtistId' property should be required.")]
    public Guid ArtistId { get; set; }

    [ForeignKey("Genres")]
    [RequiredGuid(ErrorMessage = "The 'GenreId' property should be required.")]
    public Guid GenreId { get; set; }

    [ForeignKey("ArtistId")] public virtual ArtistDatabaseModel Artist { get; set; } = null!;

    [ForeignKey("GenreId")] public virtual GenreDatabaseModel Genre { get; set; } = null!;
}