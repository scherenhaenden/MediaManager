using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeOneSender.Data.Domain;

[Table("Genre")]
public class Genre
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [RequiredGuid(ErrorMessage = "The 'Id' property should be required.")]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }
}
