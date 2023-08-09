namespace BeOneSender.Gui.Aspnet.Models;

public class SongViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ArtistViewModel Artist { get; set; } = new();
    public GenreViewModel Genre { get; set; } = new();

    public string Path { get; set; }
}