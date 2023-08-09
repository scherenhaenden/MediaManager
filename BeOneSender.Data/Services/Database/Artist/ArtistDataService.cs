using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
using BeOneSender.Data.Mapping.Artist;
using BeOneSender.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Services.Database.Artist;

public class ArtistDataService : IArtistDataService
{
    private readonly BeOneSenderDataContext _beOneSenderDataContext;

    public ArtistDataService(BeOneSenderDataContext beOneSenderDataContext)
    {
        _beOneSenderDataContext = beOneSenderDataContext;
    }

    public async Task<ArtistDataModel> AddArtistAsync(string artist, CancellationToken cancellationToken = default)
    {
        // map the artist to the database model
        var artistDatabaseModel = new ArtistDatabaseModel
        {
            Id = Guid.NewGuid(),
            Name = artist
        };

        artistDatabaseModel = (await _beOneSenderDataContext.Artists.AddAsync(artistDatabaseModel, cancellationToken))
            .Entity;
        await _beOneSenderDataContext.SaveChangesAsync(cancellationToken);

        return MapDataModelInOutDatabaseModel.Map(artistDatabaseModel);
    }

    public async Task<ArtistDataModel?> GetArtistAsync(string artist, CancellationToken cancellationToken = default)
    {
        var value = await _beOneSenderDataContext.Artists.FirstOrDefaultAsync(x => x.Name == artist, cancellationToken);

        if (value == null)
            return null;

        return MapDataModelInOutDatabaseModel.Map(value);
    }


    public async Task<ArtistDataModel> GetOrAddArtistAsync(string artist, CancellationToken cancellationToken)
    {
        var artistDataModel = await GetArtistAsync(artist, cancellationToken);

        if (artistDataModel == null)
            // add artist
            artistDataModel = await AddArtistAsync(artist, cancellationToken);

        return artistDataModel;
    }

    public async Task<List<ArtistDataModel>> GetArtistsByPatternAsync(string pattern = "",
        CancellationToken cancellationToken = default)
    {
        var artists = await _beOneSenderDataContext
            .Artists.Where(x => x.Name.ToLower().Contains(pattern.ToLower())
                                || pattern.ToLower().Contains(x.Name.ToLower())
            )?.ToListAsync(cancellationToken)!;

        return MapDataModelInOutDatabaseModel.Map(artists!);
    }

    public async Task<ArtistDataModel?> GetArtistByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _beOneSenderDataContext.Artists.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (result == null)
            return null;

        // map
        return MapDataModelInOutDatabaseModel.Map(result!);
    }
}