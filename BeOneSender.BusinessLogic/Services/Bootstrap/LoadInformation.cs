using BeOneSender.BusinessLogic.Models;
using BeOneSender.BusinessLogic.Services.Genre;
using BeOneSender.BusinessLogic.Services.SongLibraryManagement;
using BeOneSender.Data.Database.Core.Configuration;
using BootstrapingHelperCli.Tools;
using Newtonsoft.Json;

namespace BeOneSender.BusinessLogic.Services.Bootstrap;

public class LoadInformation : ILoadInformation
{
    private readonly BeOneSenderDataContext _beOneSenderDataContext;

    public LoadInformation(BeOneSenderDataContext beOneSenderDataContext)
    {
        _beOneSenderDataContext = beOneSenderDataContext;
    }

    public async Task LoadData(string path)
    {
        var currentPath = Directory.GetCurrentDirectory();
        var csvPath = Path.Combine(currentPath, path, "Assets");


        var files = Directory.GetFiles(csvPath);

        Console.WriteLine("currentPath: " + currentPath);

        ICsvToJsonTransformation csvToJsonTransformation = new CsvToJsonTransformation();

        var json = csvToJsonTransformation.Transform(files[0]);


        var rawListSongs = JsonConvert.DeserializeObject<List<RawListSongs>>(json);

        // take only 10 from rawListSongs
        //rawListSongs = rawListSongs.Take(100).ToList();

        var listSongs = new List<SongBusinessLogicModel>();

        IGenreManagerService genreManagerService = new GenreManagerService(_beOneSenderDataContext);

        genreManagerService.AddGenre("Rock", "Rock");
        genreManagerService.AddGenre("Pop", "Pop");
        genreManagerService.AddGenre("Latin", "Latin");
        genreManagerService.AddGenre("Dance", "Dance");

        var genres = await genreManagerService.GetAllGenres();


        ISongsManagerService songsManagerService = new SongsManagerService(_beOneSenderDataContext);
        var random = new Random();

        var intdex = 0;

        var listSongsToAdd = new List<InputSongModel>();

        foreach (var song in rawListSongs)
        {
            var index = random.Next(genres.Count);

            intdex++;

            listSongsToAdd.Add(new InputSongModel
            {
                Title = song.SongClean,
                Artist = song.ARTISTCLEAN,
                Genre = genres[index].Name,
                FilePath = "C:\\Song" + intdex + ".mp3"
            });

            //songsManagerService.AddSongAsync(song.SongClean, song.ARTISTCLEAN, genres[index].Name, "C:\\Song1.mp3");
        }

        songsManagerService.AddSongsAsync(listSongsToAdd);

        var songs = await songsManagerService.GetAllSongsAsync();
    }

    public async Task LoadData()
    {
        var currentPath = Directory.GetCurrentDirectory();
        var csvPath = Path.Combine(currentPath, "..", "..", "..", "..", "Assets");


        var files = Directory.GetFiles(csvPath);

        Console.WriteLine("currentPath: " + currentPath);

        ICsvToJsonTransformation csvToJsonTransformation = new CsvToJsonTransformation();

        var json = csvToJsonTransformation.Transform(files[0]);


        var rawListSongs = JsonConvert.DeserializeObject<List<RawListSongs>>(json);

        // take only 10 from rawListSongs
        //rawListSongs = rawListSongs.Take(100).ToList();

        var listSongs = new List<SongBusinessLogicModel>();

        IGenreManagerService genreManagerService = new GenreManagerService(_beOneSenderDataContext);

        genreManagerService.AddGenre("Rock", "Rock");
        genreManagerService.AddGenre("Pop", "Pop");
        genreManagerService.AddGenre("Latin", "Latin");
        genreManagerService.AddGenre("Dance", "Dance");

        var genres = await genreManagerService.GetAllGenres();


        ISongsManagerService songsManagerService = new SongsManagerService(_beOneSenderDataContext);
        var random = new Random();

        var intdex = 0;

        var listSongsToAdd = new List<InputSongModel>();

        foreach (var song in rawListSongs)
        {
            var index = random.Next(genres.Count);

            intdex++;

            listSongsToAdd.Add(new InputSongModel
            {
                Title = song.SongClean,
                Artist = song.ARTISTCLEAN,
                Genre = genres[index].Name,
                FilePath = "C:\\Song" + intdex + ".mp3"
            });

            //songsManagerService.AddSongAsync(song.SongClean, song.ARTISTCLEAN, genres[index].Name, "C:\\Song1.mp3");
        }

        songsManagerService.AddSongsAsync(listSongsToAdd);

        var songs = await songsManagerService.GetAllSongsAsync();
    }
}