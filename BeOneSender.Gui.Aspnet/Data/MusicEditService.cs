using BeOneSender.BusinessLogic.Models;
using BeOneSender.BusinessLogic.Services.SongLibraryManagement;
using BeOneSender.Gui.Aspnet.Models;

namespace BeOneSender.Gui.Aspnet.Data;

public class MusicEditService : IMusicEditService
{
    private readonly IConfiguration _configuration;
    private readonly ISongsManagerService _songsManagerService;

    public MusicEditService(IConfiguration configuration, ISongsManagerService songsManagerService)
    {
        _configuration = configuration;
        _songsManagerService = songsManagerService;
    }

    public async Task<SongViewModel> LoadSongInformationByIdAsync(Guid songId,
        CancellationToken cancellationToken = default)
    {
        var model = await _songsManagerService.GetSongByIdAsync(songId, cancellationToken);

        // map
        return new SongViewModel
        {
            Id = model.Id,
            Title = model.Title,
            Artist = new ArtistViewModel
            {
                ArtistId = model.ArtistBusinessLogicModel.Id,
                Name = model.ArtistBusinessLogicModel.Name
            },

            Genre = new GenreViewModel
            {
                GenreId = model.GenreBusinessLogicModel.Id,
                Name = model.GenreBusinessLogicModel.Name
            },
            Path = model.Path
        };
    }

    public async Task<ArtistViewModel> GetArtistByIdAsync(Guid songId, CancellationToken cancellationToken = default)
    {
        var result = await _songsManagerService.GetArtistByGuid(songId, cancellationToken);

        // map
        return new ArtistViewModel
        {
            ArtistId = result.Id,
            Name = result.Name
        };
    }

    public async Task<SongViewModel> AddSongAsync(string title, string artist, string genre, string filePath,
        CancellationToken cancellationToken = default)
    {
        var logicModel = await _songsManagerService.AddSongAsync(title, artist, genre, filePath, cancellationToken);

        // map
        return new SongViewModel
        {
            Id = logicModel.Id,
            Title = logicModel.Title,
            Artist = new ArtistViewModel
            {
                ArtistId = logicModel.Id,
                Name = logicModel.ArtistBusinessLogicModel.Name
            },
            Genre =
            {
                GenreId = logicModel.GenreBusinessLogicModel.Id,
                Name = logicModel.GenreBusinessLogicModel.Name
            },
            Path = logicModel.Path
        };
    }

    public Task<SongViewModel> AddSongAsync(SongViewModel songBusinessLogicModel,
        CancellationToken cancellationToken = default)
    {
        // map
        var logicModel = new SongBusinessLogicModel
        {
            Id = songBusinessLogicModel.Id,
            Title = songBusinessLogicModel.Title,
            ArtistBusinessLogicModel = new ArtistBusinessLogicModel
            {
                Name = songBusinessLogicModel.Artist.Name,
                Id = songBusinessLogicModel.Artist.ArtistId
            },
            GenreBusinessLogicModel = new GenreBusinessLogicModel
            {
                Name = songBusinessLogicModel.Genre.Name,
                Id = songBusinessLogicModel.Genre.GenreId
            },
            Path = songBusinessLogicModel.Path
        };

        return AddSongAsync(logicModel.Title, logicModel.ArtistBusinessLogicModel.Name,
            logicModel.GenreBusinessLogicModel.Name, logicModel.Path, cancellationToken);
    }

    public async Task DeleteSongByIdAsync(Guid songId, CancellationToken cancellationToken = default)
    {
        await _songsManagerService.DeleteSong(songId, cancellationToken);
    }

    public async Task<SongViewModel> UpdateSongAsync(SongViewModel songBusinessLogicModel,
        CancellationToken cancellationToken = default)
    {
        // map
        var logicModel = new SongBusinessLogicModel
        {
            Id = songBusinessLogicModel.Id,
            Title = songBusinessLogicModel.Title,
            ArtistBusinessLogicModel = new ArtistBusinessLogicModel
            {
                Name = songBusinessLogicModel.Artist.Name,
                Id = songBusinessLogicModel.Artist.ArtistId
            },
            GenreBusinessLogicModel = new GenreBusinessLogicModel
            {
                Name = songBusinessLogicModel.Genre.Name,
                Id = songBusinessLogicModel.Genre.GenreId
            },
            Path = songBusinessLogicModel.Path
        };

        await _songsManagerService.UpdateSong(logicModel, cancellationToken);

        return songBusinessLogicModel;
    }

    public Task<SongViewModel> UpdateSongAsync(SongBusinessLogicModel songBusinessLogicModel,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}