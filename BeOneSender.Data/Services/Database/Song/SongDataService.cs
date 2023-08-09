using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
using BeOneSender.Data.Mapping.Artist;
using BeOneSender.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Services.Database.Song;

public class SongDataService : ISongDataService
{
    private readonly BeOneSenderDataContext _beOneSenderDataContext;

    public SongDataService(BeOneSenderDataContext beOneSenderDataContext)
    {
        _beOneSenderDataContext = beOneSenderDataContext;
    }

    public async Task<PaginationSongDataModel> GetSongsByMatch(string titleName,
        CancellationToken cancellationToken = default)
    {
        var songs = await _beOneSenderDataContext.Songs.CountAsync(cancellationToken);


        var listOfSongs = await _beOneSenderDataContext
            .Songs.Include(s => s.Artist)
            .Include(s => s.Genre)
            .Where(x =>
                x.Title.ToLower().Contains(titleName.ToLower())
                //|| x.ArtistDatabaseModel.Name.ToLower().Contains(titleName.ToLower())
                //|| x.GenreDatabaseModel.Name.ToLower().Contains(titleName.ToLower())
                //|| EF.Functions.Like(x.Title, $"%{titleName}%")
                || titleName.ToLower().Contains(x.Title.ToLower())
            )
            .ToListAsync(cancellationToken);
        if (listOfSongs == null)
            return null;

        // map 
        var listOfSongsDataModel = MapDataModelInOutDatabaseModel.Map(listOfSongs);

        var paginationSongDataModel = new PaginationSongDataModel
        {
            TotalSongs = songs,
            Songs = listOfSongsDataModel
        };


        return paginationSongDataModel;
    }

    public async Task<SongDataModel?> GetSongById(Guid id, CancellationToken cancellationToken = default)
    {
        var model = await _beOneSenderDataContext.Songs
            .Include(s => s.Artist)
            .Include(s => s.Genre)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
            return null;

        // map
        return MapDataModelInOutDatabaseModel.Map(model);
    }

    public async Task<SongDataModel> UpdateSongByModel(SongDataModel songDataModel,
        CancellationToken cancellationToken = default)
    {
        // map from data model to database model
        var dataBaseModel = MapToSongDatabaseModel(songDataModel);

        // update database
        try
        {
            //_beOneSenderDataContext.Entry(dataBaseModel).CurrentValues.SetValues(dataBaseModel);
            _beOneSenderDataContext.Songs.Update(dataBaseModel);

            /*var existingEntity = _beOneSenderDataContext.Songs.Find(dataBaseModel.Id);
            if (existingEntity != null)
            {


            }*/

            await _beOneSenderDataContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // save changes
        await _beOneSenderDataContext.SaveChangesAsync();

        return MapDataModelInOutDatabaseModel.Map(dataBaseModel);
    }

    public async Task<SongDataModel> AddSongAsync(SongDataModel songDataModel,
        CancellationToken cancellationToken = default)
    {
        // map from data model to database model
        var dataBaseModel = new SongDatabaseModel();

        dataBaseModel.Id = Guid.NewGuid();
        dataBaseModel.Title = songDataModel.Title;
        dataBaseModel.Path = songDataModel.Path;
        dataBaseModel.GenreId = songDataModel.Genre.Id;
        dataBaseModel.ArtistId = songDataModel.Artist.Id;

        // add to database
        var entity = (await _beOneSenderDataContext.Songs.AddAsync(dataBaseModel, cancellationToken)).Entity;

        // save changes
        await _beOneSenderDataContext.SaveChangesAsync();

        // map from database model to data model

        return MapDataModelInOutDatabaseModel.Map(entity);
    }

    public async Task DeleteSong(Guid songId, CancellationToken cancellationToken = default)
    {
        var dataModel = await _beOneSenderDataContext.Songs.FirstOrDefaultAsync(x => x.Id == songId, cancellationToken);

        if (dataModel == null)
            return;

        _beOneSenderDataContext.Songs.Remove(dataModel);
        _beOneSenderDataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PaginationSongDataModel> GetAllSongsByPaginationAsync(int take, int skip,
        CancellationToken cancellationToken = default)
    {
        var totalcounCountAsync = await _beOneSenderDataContext.Songs.CountAsync(cancellationToken);


        var listOfSongs = await _beOneSenderDataContext
            .Songs.Skip(skip).Take(take)
            .Include(s => s.Artist)
            .Include(s => s.Genre)
            .ToListAsync(cancellationToken);

        var listOfSongsDataModel = MapDataModelInOutDatabaseModel.Map(listOfSongs);

        var paginationSongDataModel = new PaginationSongDataModel
        {
            TotalSongs = totalcounCountAsync,
            Songs = listOfSongsDataModel
        };

        return paginationSongDataModel;
    }

    public async Task<PaginationSongDataModel> GetAllSongsByPaginationAsyncAndQueryParameters(int take, int skip,
        string title, string artist, Guid? genreId,
        CancellationToken cancellationToken)
    {
        var listOfSongs = await _beOneSenderDataContext.Songs.Include(s => s.Artist)
            .Include(s => s.Genre)
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(title))
            listOfSongs = listOfSongs.Where(x =>
                x.Title.ToLower().Contains(title.ToLower())
                || title.ToLower().Contains(x.Title.ToLower())
            ).ToList();

        if (!string.IsNullOrWhiteSpace(artist))
            listOfSongs = listOfSongs.Where(x =>
                x.Artist.Name.ToLower().Contains(artist.ToLower())
                || artist.ToLower().Contains(x.Artist.Name.ToLower())
            ).ToList();

        if (genreId != Guid.Empty && genreId != null)
            listOfSongs = listOfSongs.Where(x => x.GenreId == genreId).ToList();

        var totalcounCountAsync = listOfSongs.Count();

        listOfSongs = listOfSongs.OrderBy(x => x.Id).Skip(skip).Take(take).ToList();

        var values = MapDataModelInOutDatabaseModel.Map(listOfSongs);

        var paginationSongDataModel = new PaginationSongDataModel
        {
            TotalSongs = totalcounCountAsync,
            Songs = values
        };

        return paginationSongDataModel;
    }


    private static SongDatabaseModel MapToSongDatabaseModel(SongDataModel songDataModel)
    {
        return MapDataModelInOutDatabaseModel.Map(songDataModel);
    }
}