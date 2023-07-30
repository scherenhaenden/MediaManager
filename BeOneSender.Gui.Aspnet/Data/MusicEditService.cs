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
            Artist = model.ArtistBusinessLogicModel.Name,
            Genre = model.GenreBusinessLogicModel.Name,
            Path = model.Path
        };
    }

    public Task<SongViewModel> AddSongAsync(string title, string artist, string genre, string filePath,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSongByIdAsync(Guid songId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SongViewModel> UpdateSongAsync(SongBusinessLogicModel songBusinessLogicModel,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}