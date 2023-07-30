using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
using BeOneSender.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Services.Database.Artist;

public interface IArtistDataService
{
    Task<ArtistDataModel> AddArtistAsync(string artist, CancellationToken cancellationToken = default);

    Task<ArtistDataModel?> GetArtistAsync(string artist, CancellationToken cancellationToken = default);
    Task<ArtistDataModel?> GetArtistById(Guid artistId, CancellationToken cancellationToken = default);
    Task<ArtistDataModel> GetOrAddArtistAsync(string elementArtist, CancellationToken cancellationToken);
}

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

        // map the database model to the data model
        var artistDataModel = new ArtistDataModel
        {
            Id = artistDatabaseModel.Id,
            Name = artistDatabaseModel.Name
        };


        return artistDataModel;
    }

    public async Task<ArtistDataModel?> GetArtistAsync(string artist, CancellationToken cancellationToken = default)
    {
        var value = await _beOneSenderDataContext.Artists.FirstOrDefaultAsync(x => x.Name == artist, cancellationToken);

        if (value == null)
            return null;

        var artistDataModel = new ArtistDataModel();

        artistDataModel.Id = value.Id;
        artistDataModel.Name = value.Name;

        return artistDataModel;
    }

    public async Task<ArtistDataModel?> GetArtistById(Guid artistId, CancellationToken cancellationToken = default)
    {
        var artist = await _beOneSenderDataContext
            .Artists.FirstOrDefaultAsync(x => x.Id == artistId, cancellationToken);

        if (artist == null)
            return null;

        var artistDataModel = new ArtistDataModel();

        artistDataModel.Id = artist.Id;
        artistDataModel.Name = artist.Name;

        return artistDataModel;
    }

    public async Task<ArtistDataModel> GetOrAddArtistAsync(string artist, CancellationToken cancellationToken)
    {
        var artistDataModel = await GetArtistAsync(artist, cancellationToken);

        if (artistDataModel == null)
            // add artist
            artistDataModel = await AddArtistAsync(artist, cancellationToken);

        return artistDataModel;
    }
}