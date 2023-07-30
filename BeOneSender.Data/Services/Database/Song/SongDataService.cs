using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Database.Domain;
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
        var songs = await _beOneSenderDataContext.Songs.CountAsync();


        var listOfSongs = await _beOneSenderDataContext
            .Songs.Include(s => s.ArtistDatabaseModel)
            .Include(s => s.GenreDatabaseModel)
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
        var listOfSongsDataModel = new List<SongDataModel>();

        foreach (var song in listOfSongs)
        {
            var songDataModel = MapFromSongDataBaseModel(song);

            listOfSongsDataModel.Add(songDataModel);
        }

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
            .Include(s => s.ArtistDatabaseModel)
            .Include(s => s.GenreDatabaseModel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
            return null;

        // map
        var songDataModel = MapFromSongDataBaseModel(model);
        return songDataModel;
    }

    public Task<SongDataModel> UpdateSongByModel(SongDataModel songDataModel,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<SongDataModel> AddSongAsync(SongDataModel songDataModel,
        CancellationToken cancellationToken = default)
    {
        // map from data model to database model
        var dataBaseModel = new SongDatabaseModel();

        dataBaseModel.Id = Guid.NewGuid();
        dataBaseModel.Title = songDataModel.Title;
        dataBaseModel.Path = songDataModel.Path;
        dataBaseModel.GenreId = songDataModel.GenreDataModel.Id;
        dataBaseModel.ArtistId = songDataModel.ArtistDataModel.Id;

        // add to database
        var entity = (await _beOneSenderDataContext.Songs.AddAsync(dataBaseModel, cancellationToken)).Entity;

        // save changes
        await _beOneSenderDataContext.SaveChangesAsync();

        // map from database model to data model

        var songDataModelToReturn = MapFromSongDataBaseModel(entity);

        return songDataModelToReturn;
    }

    public Task<SongDataModel> DeleteSong(SongDataModel songDataModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginationSongDataModel> GetAllSongsByPaginationAsync(int take, int skip,
        CancellationToken cancellationToken = default)
    {
        var totalcounCountAsync = await _beOneSenderDataContext.Songs.CountAsync(cancellationToken);


        var listOfSongs = await _beOneSenderDataContext
            .Songs.Skip(skip).Take(take)
            .Include(s => s.ArtistDatabaseModel)
            .Include(s => s.GenreDatabaseModel)
            .ToListAsync(cancellationToken);

        var listOfSongsDataModel = new List<SongDataModel>();

        foreach (var songs in listOfSongs) listOfSongsDataModel.Add(MapFromSongDataBaseModel(songs));


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

        var listOfSongs = await _beOneSenderDataContext.Songs.Include(s => s.ArtistDatabaseModel)
            .Include(s => s.GenreDatabaseModel)
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(title))
        {
            listOfSongs = listOfSongs.Where(x => 
                x.Title.ToLower().Contains(title.ToLower())
                || title.ToLower().Contains(x.Title.ToLower())
                ).ToList();
        }
        
        if (!string.IsNullOrWhiteSpace(artist))
        {
            listOfSongs = listOfSongs.Where(x => 
                x.ArtistDatabaseModel.Name.ToLower().Contains(artist.ToLower())
                || artist.ToLower().Contains(x.ArtistDatabaseModel.Name.ToLower())
                ).ToList();
        }
        
        if (genreId != Guid.Empty && genreId != null)
        {
            listOfSongs = listOfSongs.Where(x => x.GenreId == genreId).ToList();
        }
        
        var totalcounCountAsync = listOfSongs.Count();
        
        listOfSongs = listOfSongs.OrderBy(x=>x.Id).Skip(skip).Take(take).ToList();
        
        

        var listOfSongsDataModel = new List<SongDataModel>();

        foreach (var songs in listOfSongs) listOfSongsDataModel.Add(MapFromSongDataBaseModel(songs));


        var paginationSongDataModel = new PaginationSongDataModel
        {
            TotalSongs = totalcounCountAsync,
            Songs = listOfSongsDataModel
        };

        return paginationSongDataModel;
    }

    private static SongDataModel MapFromSongDataBaseModel(SongDatabaseModel song)
    {
        var songDataModel = new SongDataModel
        {
            Id = song.Id,
            Title = song.Title,
            Path = song.Path,
            ArtistDataModel = new ArtistDataModel
            {
                Id = song.ArtistDatabaseModel.Id,
                Name = song.ArtistDatabaseModel.Name
            },
            GenreDataModel = new GenreDataModel
            {
                Id = song.GenreDatabaseModel.Id,
                Name = song.GenreDatabaseModel.Name
            }
        };
        return songDataModel;
    }
}