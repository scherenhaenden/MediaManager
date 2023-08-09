using BeOneSender.Data.Database.Domain;
using BeOneSender.Data.Models;

namespace BeOneSender.Data.Mapping.Artist;

public partial class MapDataModelInOutDatabaseModel
{
    public static ArtistDataModel Map(ArtistDatabaseModel entity)
    {
        return new ArtistDataModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static ArtistDatabaseModel Map(ArtistDataModel entity)
    {
        return new ArtistDatabaseModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static List<ArtistDataModel> Map(List<ArtistDatabaseModel> entities)
    {
        // map list
        return entities.Select(Map).ToList();
    }

    public static List<ArtistDatabaseModel> Map(List<ArtistDataModel> entities)
    {
        return entities.Select(Map).ToList();
    }
}

public partial class MapDataModelInOutDatabaseModel
{
    public static GenreDataModel Map(GenreDatabaseModel artist)
    {
        return new GenreDataModel
        {
            Id = artist.Id,
            Name = artist.Name
        };
    }

    public static GenreDatabaseModel Map(GenreDataModel artist)
    {
        return new GenreDatabaseModel
        {
            Id = artist.Id,
            Name = artist.Name
        };
    }

    public static List<GenreDataModel> Map(List<GenreDatabaseModel> entities)
    {
        // map list
        return entities.Select(Map).ToList();
    }

    public static List<GenreDatabaseModel> Map(List<GenreDataModel> entities)
    {
        return entities.Select(Map).ToList();
    }
}

public partial class MapDataModelInOutDatabaseModel
{
    public static SongDataModel Map(SongDatabaseModel entity)
    {
        return new SongDataModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Path = entity.Path,
            Artist = Map(entity.Artist),
            Genre = Map(entity.Genre)
        };
    }

    public static SongDatabaseModel Map(SongDataModel entity)
    {
        return new SongDatabaseModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Path = entity.Path,
            Artist = Map(entity.Artist),
            Genre = Map(entity.Genre)
        };
    }

    public static List<SongDataModel> Map(List<SongDatabaseModel> entities)
    {
        // map list
        return entities.Select(Map).ToList();
    }

    public static List<SongDatabaseModel> Map(List<SongDataModel> entities)
    {
        return entities.Select(Map).ToList();
    }
}