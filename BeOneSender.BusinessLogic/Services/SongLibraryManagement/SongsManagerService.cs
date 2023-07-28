using BeOneSender.BusinessLogic.Models;

namespace BeOneSender.BusinessLogic.Services.SongsManagement;

public class SongsManagerService : ISongsManagerService
{
    public void AddSong(string title, string artist, string genre, string filePath)
    {
        throw new NotImplementedException();
    }

    public List<SongBusinessLogicModel> GetAllSongs()
    {
        throw new NotImplementedException();
    }

    public List<SongBusinessLogicModel> GetSongsByCriteria(string title = null, string artist = null, string genre = null)
    {
        throw new NotImplementedException();
    }

    public void UpdateSong(Guid songId, string title, string artist, string genre, string filePath)
    {
        throw new NotImplementedException();
    }

    public void DeleteSong(Guid songId)
    {
        throw new NotImplementedException();
    }

    public List<string> GetAvailableGenres()
    {
        throw new NotImplementedException();
    }
}