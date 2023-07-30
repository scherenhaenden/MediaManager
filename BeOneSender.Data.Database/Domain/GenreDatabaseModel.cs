using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SharedTools.Validation.Attributes;

namespace BeOneSender.Data.Database.Domain;

[Table("Genres")]
[Index(nameof(Name), IsUnique = true)]
public class GenreDatabaseModel
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]
    [Key]
    public Guid Id { get; set; }


    [Required] [StringLength(100)] public string Name { get; set; } = null!;

    public virtual ICollection<SongDatabaseModel> SongDatabaseModel { get; } = new List<SongDatabaseModel>();

    public string? Description { get; set; }
}