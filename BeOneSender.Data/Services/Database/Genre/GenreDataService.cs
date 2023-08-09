using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
using BeOneSender.Data.Mapping.Artist;
using BeOneSender.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Services.Database.Genre;

public class GenreDataService : IGenreDataService
{
    private readonly BeOneSenderDataContext _beOneSenderDataContext;

    public GenreDataService(BeOneSenderDataContext beOneSenderDataContext)
    {
        _beOneSenderDataContext = beOneSenderDataContext;
    }

    public async Task<List<GenreDataModel>> GetGenresAsync(CancellationToken cancellationToken = default)
    {
        var databaseModel = await _beOneSenderDataContext.Genres.ToListAsync(cancellationToken);


        return databaseModel.Select(x => new GenreDataModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToList();
    }

    public async Task<GenreDataModel> AddGenre(string genre, string? description,
        CancellationToken cancellationToken = default)
    {
        var databaseModel = new GenreDatabaseModel
        {
            Id = Guid.NewGuid(),
            Name = genre
        };

        var entity = (await _beOneSenderDataContext.Genres.AddAsync(databaseModel)).Entity;

        _beOneSenderDataContext.SaveChanges();

        var datamodel = new GenreDataModel();

        datamodel.Id = entity.Id;
        datamodel.Name = entity.Name;
        datamodel.Description = entity.Description;
        return datamodel;
    }

    public async Task<GenreDataModel?> GetGenreById(Guid id, CancellationToken cancellationToken = default)
    {
        var databaseModel =
            await _beOneSenderDataContext.Genres.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (databaseModel == null) return null;

        var dataModel = new GenreDataModel();

        dataModel.Id = databaseModel.Id;
        dataModel.Name = databaseModel.Name;

        return dataModel;
    }

    public async Task<GenreDataModel?> GetGenreAsync(string genre, CancellationToken cancellationToken = default)
    {
        var databaseModel = await _beOneSenderDataContext.Genres
            .FirstOrDefaultAsync(g => g.Name == genre, cancellationToken);

        if (databaseModel == null)
            return null;

        return MapDataModelInOutDatabaseModel.Map(databaseModel);
    }

    public async Task<GenreDataModel?> GetGenreByMatch(string genre, CancellationToken cancellationToken = default)
    {
        var databaseModel = await _beOneSenderDataContext.Genres
            .FirstOrDefaultAsync(
                g =>
                    g.Name.ToLower().Contains(genre.ToLower())
                    ||
                    genre.ToLower().Contains(g.Name.ToLower())
                , cancellationToken);

        if (databaseModel == null)
            return null;

        return MapDataModelInOutDatabaseModel.Map(databaseModel);
    }

    public async Task<GenreDataModel> AddOrGetGenre(string genre, string? description,
        CancellationToken cancellationToken = default)
    {
        // Check if the genre already exists in the database
        var datamodel = await GetGenreAsync(genre, cancellationToken);

        if (datamodel == null)
            // Genre does not exist, create a new genre entry
            datamodel = await AddGenre(genre, description, cancellationToken);

        return datamodel;
    }
}