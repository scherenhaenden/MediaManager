using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedTools.Validation.Attributes;

namespace BeOneSender.Data.Database.Domain;

[Table("Artists")]
public class ArtistDatabaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]
    public Guid Id { get; set; }

    [Required] [StringLength(500)] public string Name { get; set; } = null!;

    public virtual ICollection<SongDatabaseModel> SongDatabaseModel { get; } = new List<SongDatabaseModel>();
}