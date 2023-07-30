using BeOneSender.BusinessLogic.Models;

namespace BeOneSender.BusinessLogic.Services.Genre;

public interface IGenreManagerService
{
    Task<GenreBusinessLogicModel> AddGenre(string genre, string? description,
        CancellationToken cancellationToken = default);

    Task<List<GenreBusinessLogicModel>> GetAllGenres(
        CancellationToken cancellationToken = default);
}