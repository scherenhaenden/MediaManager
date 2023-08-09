using BeOneSender.BusinessLogic.Models;
using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Data.Services.Database.Genre;

namespace BeOneSender.BusinessLogic.Services.Genre;

public class GenreManagerService : IGenreManagerService
{
    private readonly BeOneSenderDataContext _beOneSenderDataContext;
    private readonly IGenreDataService _genreDataService;

    public GenreManagerService(IGenreDataService genreDataService)
    {
        _genreDataService = genreDataService;
    }

    public GenreManagerService(BeOneSenderDataContext beOneSenderDataContext)
    {
        _beOneSenderDataContext = beOneSenderDataContext;
        _genreDataService = new GenreDataService(beOneSenderDataContext);
    }

    public async Task<GenreBusinessLogicModel> AddGenre(string genre, string? description,
        CancellationToken cancellationToken = default)
    {
        var result = await _genreDataService.AddOrGetGenre(genre, description, cancellationToken);

        return new GenreBusinessLogicModel
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description
        };
    }

    public async Task<List<GenreBusinessLogicModel>> GetAllGenres(CancellationToken cancellationToken = default)
    {
        var dataModels = await _genreDataService.GetGenresAsync(cancellationToken);

        return dataModels.Select(x => new GenreBusinessLogicModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToList();
    }
}