using System.ComponentModel.DataAnnotations;

namespace BeOneSender.Data.Models;

public class SongDataModel
{
    public Guid Id { get; set; }

    [Required] [StringLength(500)] public string Title { get; set; } = null!;

    [Required] [StringLength(500)] public string Path { get; set; } = null!;

    [Required] public ArtistDataModel ArtistDataModel { get; set; } = null!;

    [Required] public GenreDataModel GenreDataModel { get; set; } = null!;
}