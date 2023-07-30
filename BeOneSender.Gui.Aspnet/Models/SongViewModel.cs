namespace BeOneSender.Gui.Aspnet.Models;

public class SongViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }

    public string Path { get; set; }
}