using BeOneSender.Data.Models;

namespace BeOneSender.Data.Services.Database.Artist;

public interface IArtistDataService
{
    Task<ArtistDataModel> AddArtistAsync(string artist, CancellationToken cancellationToken = default);

    Task<ArtistDataModel?> GetArtistAsync(string artist, CancellationToken cancellationToken = default);
    Task<ArtistDataModel> GetOrAddArtistAsync(string elementArtist, CancellationToken cancellationToken);

    Task<List<ArtistDataModel>> GetArtistsByPatternAsync(string pattern = "",
        CancellationToken cancellationToken = default);

    Task<ArtistDataModel?> GetArtistByGuidAsync(Guid id, CancellationToken cancellationToken);
}