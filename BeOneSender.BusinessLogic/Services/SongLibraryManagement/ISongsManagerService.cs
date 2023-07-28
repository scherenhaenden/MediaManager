using BeOneSender.BusinessLogic.Models;

namespace BeOneSender.BusinessLogic.Services.SongsManagement;

public interface ISongsManagerService
{
    // Add a new song to the music storage database.
    void AddSongAsync(string title, string artist, string genre, string filePath);

    // Get a list of all songs in the music storage database.
    List<SongBusinessLogicModel> GetAllSongs();

    // Get a list of songs that match the given title, artist, and genre.
    List<SongBusinessLogicModel> GetSongsByCriteria(string title = null, string artist = null, string genre = null);

    // Update the details of an existing song in the music storage database.
    void UpdateSong(Guid songId, string title, string artist, string genre, string filePath);

    // Delete a song from the music storage database.
    void DeleteSong(Guid songId);

    // Get a list of available genres.
    List<string> GetAvailableGenres();
}