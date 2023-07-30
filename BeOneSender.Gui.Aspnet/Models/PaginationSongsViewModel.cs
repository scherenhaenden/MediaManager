namespace BeOneSender.Gui.Aspnet.Models;

public class PaginationSongsViewModel
{
    public List<SongViewModel> Songs { get; set; } = new();

    public int TotalSongs { get; set; }
}