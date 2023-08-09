using BeOneSender.BusinessLogic.Services.SongLibraryManagement;
using BeOneSender.Gui.Aspnet.Models;

namespace BeOneSender.Gui.Aspnet.Data;

public class MusicSearchService : IMusicSearchService
{
    private readonly IConfiguration _configuration;
    private readonly ISongsManagerService _songsManagerService;

    public MusicSearchService(IConfiguration configuration, ISongsManagerService songsManagerService)
    {
        _configuration = configuration;
        _songsManagerService = songsManagerService;
    }

    public async Task<PaginationSongsViewModel> GetAllSongsByPaginationAsyncAndQueryParameters(int take, int skip,
        string title, string artist, Guid? genreId,
        CancellationToken cancellationToken = default)
    {
        var paginationSongBusinessLogic =
            await _songsManagerService.GetAllSongsByPaginationAsyncAndQueryParameters(take, skip, title, artist,
                genreId, cancellationToken);

        // map
        var paginationSongViewModel = new PaginationSongsViewModel
        {
            Songs = paginationSongBusinessLogic.Songs.Select(songBusinessLogicModel => new SongViewModel
            {
                Id = songBusinessLogicModel.Id,
                Title = songBusinessLogicModel.Title,
                Artist = new ArtistViewModel
                {
                    ArtistId = songBusinessLogicModel.ArtistBusinessLogicModel.Id,
                    Name = songBusinessLogicModel.ArtistBusinessLogicModel.Name
                },

                Genre = new GenreViewModel
                {
                    GenreId = songBusinessLogicModel.GenreBusinessLogicModel.Id,
                    Name = songBusinessLogicModel.GenreBusinessLogicModel.Name
                },
                Path = songBusinessLogicModel.Path
            }).ToList(),
            TotalSongs = paginationSongBusinessLogic.TotalSongs
        };

        return paginationSongViewModel;
    }

    public Task<PaginationSongsViewModel> SearchSongsByTitleOrInterpretOrGenre(int take, int skip, string searchQuery,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationSongsViewModel> SearchSongsByTitle(int take, int skip, string title,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationSongsViewModel> SearchSongsByArtist(int take, int skip, string artist,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationSongsViewModel> SearchSongsByGenre(int take, int skip, string genre,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GenreViewModel>> GetAllGenresAsync(CancellationToken cancellationToken = default)
    {
        var model = await _songsManagerService.GetGenresAsync(cancellationToken);

        // map
        var genreViewModel = model.Select(genreBusinessLogicModel => new GenreViewModel
        {
            GenreId = genreBusinessLogicModel.Id,
            Name = genreBusinessLogicModel.Name
        }).ToList();

        return genreViewModel;
    }

    public async Task<List<ArtistViewModel>> GetAllArtistsAsync(string pattern = "",
        CancellationToken cancellationToken = default)
    {
        var model = await _songsManagerService.GetArtistsByPatternAsync(pattern, cancellationToken);

        // map
        var genreViewModel = model.Select(genreBusinessLogicModel => new ArtistViewModel
        {
            ArtistId = genreBusinessLogicModel.Id,
            Name = genreBusinessLogicModel.Name
        }).ToList();

        return genreViewModel;
    }

    public async Task<ArtistViewModel> GetArtistByGuid(Guid guid, CancellationToken cancellationToken = default)
    {
        var model = await _songsManagerService.GetArtistByGuid(guid, cancellationToken);

        // map
        var genreViewModel = new ArtistViewModel
        {
            ArtistId = model.Id,
            Name = model.Name
        };

        return genreViewModel;
    }
}