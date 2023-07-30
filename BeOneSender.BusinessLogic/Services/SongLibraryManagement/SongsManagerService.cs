using BeOneSender.BusinessLogic.Models;
using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Models;
using BeOneSender.Data.Services.Database.Artist;
using BeOneSender.Data.Services.Database.Genre;
using BeOneSender.Data.Services.Database.Song;

namespace BeOneSender.BusinessLogic.Services.SongLibraryManagement;

public class SongsManagerService : ISongsManagerService
{
    private readonly IArtistDataService _artistDataService;
    private readonly IGenreDataService _genreDataService;
    private readonly ISongDataService _songDataService;

    public SongsManagerService(BeOneSenderDataContext beOneSenderDataContext)
    {
        _genreDataService = new GenreDataService(beOneSenderDataContext);
        _songDataService = new SongDataService(beOneSenderDataContext);
        _artistDataService = new ArtistDataService(beOneSenderDataContext);
    }


    public SongsManagerService(IGenreDataService genreDataService, ISongDataService songDataService,
        IArtistDataService artistDataService)
    {
        _genreDataService = genreDataService;
        _songDataService = songDataService;
        _artistDataService = artistDataService;
    }

    public async Task<List<GenreBusinessLogicModel>> GetGenresAsync(CancellationToken cancellationToken = default)
    {
        var model = await _genreDataService.GetGenresAsync(cancellationToken);
        
        // map
        var list = model.Select(genreDataModel => new GenreBusinessLogicModel
        {
            Id = genreDataModel.Id,
            Name = genreDataModel.Name
        }).ToList();
        
        return list;
    }

    public async Task<PaginationSongBusinessLogicModel> GetSongsByName(string name, bool allowPartialMatch = true)
    {
        var songByMatch = await _songDataService.GetSongsByMatch(name);
        if (songByMatch == null)
            return null;


        var listMapped = songByMatch.Songs.Select(s => new SongBusinessLogicModel
        {
            Id = s.Id,
            Title = s.Title,
            ArtistBusinessLogicModel = new ArtistBusinessLogicModel
            {
                Id = s.ArtistDataModel.Id,
                Name = s.ArtistDataModel.Name
            },
            GenreBusinessLogicModel = new GenreBusinessLogicModel
            {
                Id = s.GenreDataModel.Id,
                Name = s.GenreDataModel.Name
            },
            Path = s.Path
        });

        var paginationSongBusinessLogicModel = new PaginationSongBusinessLogicModel
        {
            TotalSongs = songByMatch.TotalSongs,
            Songs = listMapped.ToList()
        };

        return paginationSongBusinessLogicModel;
    }

    public async Task<PaginationSongBusinessLogicModel> GetSongsByMatchingTitleArtistOrGenre(string name,
        bool allowPartialMatch = true)
    {
        var songByMatch = await _songDataService.GetSongsByMatch(name);
        if (songByMatch == null)
            return null;


        var listMapped = songByMatch.Songs.Select(songDataModel => new SongBusinessLogicModel
        {
            Id = songDataModel.Id,
            Title = songDataModel.Title,
            ArtistBusinessLogicModel = new ArtistBusinessLogicModel
            {
                Id = songDataModel.ArtistDataModel.Id,
                Name = songDataModel.ArtistDataModel.Name
            },
            GenreBusinessLogicModel = new GenreBusinessLogicModel
            {
                Id = songDataModel.GenreDataModel.Id,
                Name = songDataModel.GenreDataModel.Name
            },
            Path = songDataModel.Path
        });

        var paginationSongBusinessLogicModel = new PaginationSongBusinessLogicModel
        {
            TotalSongs = songByMatch.TotalSongs,
            Songs = listMapped.ToList()
        };

        return paginationSongBusinessLogicModel;
    }

    public async Task<bool> AddSongsAsync(List<InputSongModel> inputSongModel,
        CancellationToken cancellationToken = default)
    {
        var listOfArtist = new List<ArtistDataModel>();
        var listOfGenre = new List<GenreDataModel>();
        foreach (var element in inputSongModel)
        {
            // find artist in the list
            var artistDataModel = listOfArtist.FirstOrDefault(a => a.Name == element.Artist);

            if (null == artistDataModel)
            {
                artistDataModel = await _artistDataService.GetOrAddArtistAsync(element.Artist, cancellationToken);
                listOfArtist.Add(artistDataModel);
            }

            var genreDataModel = listOfGenre.FirstOrDefault(a => a.Name == element.Artist);
            if (null == genreDataModel)
            {
                genreDataModel = await _genreDataService.GetGenreAsync(element.Genre, cancellationToken);
                listOfGenre.Add(genreDataModel);
            }

            _songDataService.AddSongAsync(new SongDataModel
            {
                Title = element.Title,
                ArtistDataModel = artistDataModel,
                GenreDataModel = genreDataModel,
                Path = element.FilePath
            }, cancellationToken);
        }

        return true;
    }

    public async Task<PaginationSongBusinessLogicModel> GetAllSongsByPaginationAsync(int take, int skip,
        CancellationToken cancellationToken = default)
    {
        var listOfSongs = await _songDataService.GetAllSongsByPaginationAsync(take, skip, cancellationToken);

        var listOfSongsBusinessLogicModel = listOfSongs.Songs.Select(MapFromDataModel);

        return new PaginationSongBusinessLogicModel
        {
            Songs = listOfSongsBusinessLogicModel.ToList(),
            TotalSongs = listOfSongs.TotalSongs
        };
    }

    public async Task<SongBusinessLogicModel> AddSongAsync(string title, string artist, string genre, string filePath,
        CancellationToken cancellationToken = default)
    {
        // get artist
        var artistDataModel = await _artistDataService.GetArtistAsync(artist, cancellationToken);

        if (artistDataModel == null)
            // add artist
            artistDataModel = await _artistDataService.AddArtistAsync(artist, cancellationToken);

        // get genre
        var genreDataModel = await _genreDataService.GetGenreAsync(genre, cancellationToken);

        if (genreDataModel == null)
        {
            // add genre
            //genreDataModel = await _genreDataService.AddGenreAsync(genre, cancellationToken);
        }

        // map to data model
        var songDataModel = new SongDataModel
        {
            Title = title,
            ArtistDataModel = artistDataModel,
            GenreDataModel = genreDataModel,
            Path = filePath
        };


        // add song
        songDataModel = await _songDataService.AddSongAsync(songDataModel, cancellationToken);


        // map to business logic model
        return MapFromDataModel(songDataModel);
    }

    public async Task<PaginationSongBusinessLogicModel> GetAllSongsAsync()
    {
        var listOfSongs = await _songDataService.GetAllSongsByPaginationAsync(100000, 1);

        if (listOfSongs == null)
            return null;

        var listOfSongsBusinessLogicModel = listOfSongs.Songs.Select(MapFromDataModel);

        return new PaginationSongBusinessLogicModel
        {
            Songs = listOfSongsBusinessLogicModel.ToList(),
            TotalSongs = listOfSongs.TotalSongs
        };
    }

    public List<SongBusinessLogicModel> GetSongsByCriteria(string title = null, string artist = null,
        string genre = null)
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

    public async Task<SongBusinessLogicModel> GetSongByIdAsync(Guid songId,
        CancellationToken cancellationToken = default)
    {
        var songDataModel = await _songDataService.GetSongById(songId, cancellationToken);

        if (songDataModel == null)
            return null;

        return MapFromDataModel(songDataModel);
    }

    public async Task<PaginationSongBusinessLogicModel> GetAllSongsByPaginationAsyncAndQueryParameters(int take,
        int skip, string title, string artist, Guid? genreId,
        CancellationToken cancellationToken)
    {
        var listOfSongs = await _songDataService.GetAllSongsByPaginationAsyncAndQueryParameters(take, skip,title, artist, genreId, cancellationToken);

        var listOfSongsBusinessLogicModel = listOfSongs.Songs.Select(MapFromDataModel);

        return new PaginationSongBusinessLogicModel
        {
            Songs = listOfSongsBusinessLogicModel.ToList(),
            TotalSongs = listOfSongs.TotalSongs
        };
    }

    private SongBusinessLogicModel MapFromDataModel(SongDataModel songDataModel)
    {
        return new SongBusinessLogicModel
        {
            Id = songDataModel.Id,
            Title = songDataModel.Title,
            ArtistBusinessLogicModel = new ArtistBusinessLogicModel
            {
                Id = songDataModel.ArtistDataModel.Id,
                Name = songDataModel.ArtistDataModel.Name
            },
            GenreBusinessLogicModel = new GenreBusinessLogicModel
            {
                Id = songDataModel.GenreDataModel.Id,
                Name = songDataModel.GenreDataModel.Name
            },
            Path = songDataModel.Path
        };
    }

    public List<SongBusinessLogicModel> GetAllSongs()
    {
        throw new NotImplementedException();
    }
}