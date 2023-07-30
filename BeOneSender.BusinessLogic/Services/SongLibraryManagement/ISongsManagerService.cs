using BeOneSender.BusinessLogic.Models;

namespace BeOneSender.BusinessLogic.Services.SongLibraryManagement;

public interface ISongsManagerService
{
    // get all songs with take and skip
    Task<PaginationSongBusinessLogicModel> GetAllSongsByPaginationAsync(int take, int skip,
        CancellationToken cancellationToken = default);
    
    // Get a list of available genres.
    Task<List<GenreBusinessLogicModel>> GetGenresAsync(CancellationToken cancellationToken = default);


    // Get song by name (title). comparison is case-insensitive and by default, partial matching is allowed.
    Task<PaginationSongBusinessLogicModel> GetSongsByName(string name, bool allowPartialMatch = true);

    Task<PaginationSongBusinessLogicModel> GetSongsByMatchingTitleArtistOrGenre(string name,
        bool allowPartialMatch = true);


    Task<bool> AddSongsAsync(List<InputSongModel> inputSongModel, CancellationToken cancellationToken = default);


    // Add a new song to the music storage database.
    Task<SongBusinessLogicModel> AddSongAsync(string title, string artist, string genre, string filePath,
        CancellationToken cancellationToken = default);

    // Get a list of all songs in the music storage database.
    Task<PaginationSongBusinessLogicModel> GetAllSongsAsync();

    // Get a list of songs that match the given title, artist, and genre.
    List<SongBusinessLogicModel> GetSongsByCriteria(string title = null, string artist = null, string genre = null);

    // Update the details of an existing song in the music storage database.
    void UpdateSong(Guid songId, string title, string artist, string genre, string filePath);

    // Delete a song from the music storage database.
    void DeleteSong(Guid songId);

    
    Task<SongBusinessLogicModel> GetSongByIdAsync(Guid songId, CancellationToken cancellationToken = default);
    Task<PaginationSongBusinessLogicModel> GetAllSongsByPaginationAsyncAndQueryParameters(int take, int skip, string title, string artist, Guid? genreId, CancellationToken cancellationToken);
}