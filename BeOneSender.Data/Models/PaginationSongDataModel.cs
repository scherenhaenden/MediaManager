namespace BeOneSender.Data.Models;

public class PaginationSongDataModel
{
    public List<SongDataModel> Songs { get; set; } = new();
    public int TotalSongs { get; set; }
}