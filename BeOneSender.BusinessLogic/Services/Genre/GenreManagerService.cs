using BeOneSender.Data.Core.Configuration;
using BeOneSender.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.BusinessLogic.Services.SongLibraryManagement;

public class GenreManagerService : IGenreManagerService
{
    private readonly BeOneSenderDataContext _beOneSenderDataContext;

    public GenreManagerService(string connectionString)
    {
        _beOneSenderDataContext = new BeOneSenderDataContext(connectionString);
    }

    public async Task<bool> AddGenre(string title, string artist, string genre, string filePath,
        CancellationToken cancellationToken = default)
    {
        // Check if the genre already exists in the database
        var existingGenre = await _beOneSenderDataContext.Genres
            .FirstOrDefaultAsync(g => g.Name == genre, cancellationToken);

        if (existingGenre == null)
        {
            // Genre does not exist, create a new genre entry
            existingGenre = new GenreDatamodel
            {
                Id = Guid.NewGuid(),
                Name = genre
            };

            // Add the new genre to the database
            _beOneSenderDataContext.Genres.Add(existingGenre);
        }

        // Check if the artist already exists in the database
        var existingArtist = await _beOneSenderDataContext.Artists
            .FirstOrDefaultAsync(a => a.Name == artist, cancellationToken);

        if (existingArtist == null)
        {
            // Artist does not exist, create a new artist entry
            existingArtist = new ArtistDatamodel
            {
                Id = Guid.NewGuid(),
                Name = artist
            };

            // Add the new artist to the database
            _beOneSenderDataContext.Artists.Add(existingArtist);
        }

        // Create a new song entry
        var newSong = new SongDatamodel
        {
            Id = Guid.NewGuid(),
            Title = title,
            Path = filePath,
            ArtistId = existingArtist.Id,
            GenreId = existingGenre.Id
        };

        // Add the new song to the database
        _beOneSenderDataContext.Songs.Add(newSong);

        // Save changes to the database
        await _beOneSenderDataContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}