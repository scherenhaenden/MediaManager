namespace BeOneSender.BusinessLogic.Services.SongLibraryManagement;

public interface IGenreManagerService
{
    Task<bool> AddGenre(string title, string artist, string genre, string filePath, CancellationToken cancellationToken = default);

}