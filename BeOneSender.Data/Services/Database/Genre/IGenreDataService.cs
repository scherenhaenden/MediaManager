using BeOneSender.Data.Models;

namespace BeOneSender.Data.Services.Database.Genre;

public interface IGenreDataService
{
    Task<List<GenreDataModel>> GetGenresAsync(
        CancellationToken cancellationToken = default);

    Task<GenreDataModel> AddGenre(string genre, string? description,
        CancellationToken cancellationToken = default);

    Task<GenreDataModel?> GetGenreById(Guid id,
        CancellationToken cancellationToken = default);

    Task<GenreDataModel?> GetGenreAsync(string genre,
        CancellationToken cancellationToken = default);

    Task<GenreDataModel?> GetGenreByMatch(string genre,
        CancellationToken cancellationToken = default);

    Task<GenreDataModel> AddOrGetGenre(string genre, string? description,
        CancellationToken cancellationToken = default);
}