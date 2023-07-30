namespace BeOneSender.BusinessLogic.Models;

public class SongBusinessLogicModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Path { get; set; }
    public ArtistBusinessLogicModel ArtistBusinessLogicModel { get; set; }
    public GenreBusinessLogicModel GenreBusinessLogicModel { get; set; }
}