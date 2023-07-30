using BeOneSender.Data.Database.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeOneSender.Data.Database.Core.Configuration;

public class BeOneSenderDataContext : DbContext
{
    private readonly string _connectionString;

    public BeOneSenderDataContext(DbContextOptions<BeOneSenderDataContext> options)
        : base(options)
    {
        base.Database.EnsureCreated();
    }

    public BeOneSenderDataContext(string connectionString)

    {
        _connectionString = connectionString;
    }

    public DbSet<ArtistDatabaseModel> Artists { get; set; }

    public DbSet<GenreDatabaseModel> Genres { get; set; }

    public DbSet<SongDatabaseModel> Songs { get; set; }
}