using BeOneSender.Data.Models;

namespace BeOneSender.Data.Services.Database.Song;

public interface ISongDataService
{
    Task<PaginationSongDataModel> GetSongsByMatch(string titleName, CancellationToken cancellationToken = default);

    Task<SongDataModel?> GetSongById(Guid id, CancellationToken cancellationToken = default);

    Task<SongDataModel> UpdateSongByModel(SongDataModel songDataModel, CancellationToken cancellationToken = default);

    Task<SongDataModel> AddSongAsync(SongDataModel songDataModel, CancellationToken cancellationToken = default);

    Task DeleteSong(Guid songId, CancellationToken cancellationToken = default);

    // get all songs with take and skip
    Task<PaginationSongDataModel> GetAllSongsByPaginationAsync(int take, int skip,
        CancellationToken cancellationToken = default);

    Task<PaginationSongDataModel> GetAllSongsByPaginationAsyncAndQueryParameters(int take, int skip, string title,
        string artist, Guid? genreId, CancellationToken cancellationToken);
}