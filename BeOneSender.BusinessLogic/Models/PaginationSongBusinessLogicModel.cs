namespace BeOneSender.BusinessLogic.Models;

public class PaginationSongBusinessLogicModel
{
    public List<SongBusinessLogicModel> Songs { get; set; } = new();
    public int TotalSongs { get; set; }
}