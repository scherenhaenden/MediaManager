using BeOneSender.BusinessLogic.Models;
using BeOneSender.Gui.Aspnet.Models;

namespace BeOneSender.Gui.Aspnet.Data;

public interface IMusicEditService
{
    // Load song information from the music storage database.
    // Add a new song to the music storage database.
    // Delete a song from the music storage database.
    // Update the details of an existing song in the music storage database.

    public Task<SongViewModel> LoadSongInformationByIdAsync(Guid songId, CancellationToken cancellationToken = default);

    public Task<SongViewModel> AddSongAsync(string title, string artist, string genre, string filePath,
        CancellationToken cancellationToken = default);

    public Task<bool> DeleteSongByIdAsync(Guid songId, CancellationToken cancellationToken = default);

    public Task<SongViewModel> UpdateSongAsync(SongBusinessLogicModel songBusinessLogicModel,
        CancellationToken cancellationToken = default);
}