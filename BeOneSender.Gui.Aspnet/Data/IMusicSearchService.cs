using BeOneSender.Gui.Aspnet.Models;

namespace BeOneSender.Gui.Aspnet.Data;

public interface IMusicSearchService
{
    public Task<PaginationSongsViewModel> GetAllSongsByPaginationAsyncAndQueryParameters(int take, int skip,
         string? title, string? artist, Guid? genreId,
        CancellationToken cancellationToken = default);

    public Task<PaginationSongsViewModel> SearchSongsByTitleOrInterpretOrGenre(int take, int skip, string searchQuery,
        CancellationToken cancellationToken = default);

    public Task<PaginationSongsViewModel> SearchSongsByTitle(int take, int skip, string title,
        CancellationToken cancellationToken = default);

    public Task<PaginationSongsViewModel> SearchSongsByArtist(int take, int skip, string artist,
        CancellationToken cancellationToken = default);

    public Task<PaginationSongsViewModel> SearchSongsByGenre(int take, int skip, string genre,
        CancellationToken cancellationToken = default);

    public Task<List<GenreViewModel>> GetAllGenresAsync(CancellationToken cancellationToken = default);
}