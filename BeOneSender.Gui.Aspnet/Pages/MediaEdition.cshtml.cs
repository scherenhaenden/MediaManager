using System.Web;
using BeOneSender.Gui.Aspnet.Data;
using BeOneSender.Gui.Aspnet.Models;
using BootstrapingHelperCli.Tools;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeOneSender.Gui.Aspnet.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class MediaEditionModel: PageModel
{
    private readonly IMusicSearchService _musicSearchService;
    private readonly IMusicEditService _musicEditService;
    private SongViewModel _songViewModel = new();

    public SongViewModel SongViewModel
    {
        get { return _songViewModel; }
        set { _songViewModel = value; }
    }
    private List<ArtistViewModel> ArtistSuggestions { get; set; } = new();
    private ArtistViewModel SelectedArtist { get; set; } = new();
    private List<GenreViewModel> Genres { get; set; }
    private string? _selectedGenre = "";
    // Property to check if there are changes
    private bool HasChanges;
    private List<ArtistViewModel> _artists;
    private NavigationManager _navigationManager;
    public MediaEditionModel(IMusicSearchService musicSearchService,
        IMusicEditService musicEditService,
            NavigationManager navigationManager)
    {
        _musicSearchService = musicSearchService;
        _musicEditService = musicEditService;
        _navigationManager = navigationManager;
    }

    
    
    public string? SelectedGenre
    {
        get => _selectedGenre;
        set
        {
            if (value == _selectedGenre)
            {
                return;
            }
            _selectedGenre = value;
            if (Guid.TryParse(value, out var genreId))
            {
                _songViewModel.Genre = Genres
                    .FirstOrDefault(x => x.GenreId == genreId);
            }
            else
            {
                _songViewModel.Genre = new GenreViewModel();
            }
        }
    }



    public async Task LoadAllArtists()
    {
        // Load the song from the database
        // and populate the form fields
        var result = await _musicSearchService.GetAllArtistsAsync();
        _artists = result;
    }
    
    
    // onInitializedAsync
    public async Task OnGetAsync()
    {
        var uri = new Uri(_navigationManager.Uri);
        var queryParams = HttpUtility.ParseQueryString(uri.Query);

        await LoadGenres();
        await LoadAllArtists();

        if (Guid.TryParse(queryParams["musicFileId"], out var fileId))
        {
            await LoadSong(fileId);
        }
        else
        {
            _songViewModel = new SongViewModel();
        }
    }
    
    private async Task SelectArtist(ArtistViewModel? artist)
    {
        if (artist != null)
        {
            var result = await LoadArtistById(artist.ArtistId);
            _songViewModel.Artist = result;
            SelectedArtist = result;
        }
        else
        {
            _songViewModel.Artist = new ArtistViewModel();
            SelectedArtist = new ArtistViewModel();
        }

        ArtistSuggestions.Clear(); // Clear suggestions only when an artist is selected
    }
    
    public async Task<ArtistViewModel> LoadArtistById(Guid guid)
    {
        // Load the song from the database
        // and populate the form fields
        return await _musicSearchService.GetArtistByGuid(guid);
    }
    
    public bool RunAndGetValidation()
    {
        if(SelectedArtist == null)
        {
            return false;
        }
        
        if(SelectedArtist.ArtistId == Guid.Empty)
        {
            return false;
        }
        
        if(SelectedGenre == null)
        {
            return false;
        }
        
        if(SelectedGenre == "")
        {
            return false;
        }
        
        if(_songViewModel.Title == null)
        {
            return false;
        }
        
        // now path
        if(_songViewModel.Path == null)
        {
            return false;
        }

        return true;
    }

    
    public async Task SaveSong(CancellationToken cancellationToken = default)
    {
        if (!RunAndGetValidation())
        {
            return;
        }

        if (_songViewModel.Id == Guid.Empty)
        {
            _songViewModel = await _musicEditService.AddSongAsync(_songViewModel, cancellationToken);
    
        }
        else
        {
            _songViewModel = await _musicEditService.UpdateSongAsync(_songViewModel, cancellationToken);
        }
        HasChanges = false;
    }
    
    public async Task LoadSong(Guid musicFileId)
    {
        // Load the song from the database
        // and populate the form fields
        var result = await _musicEditService.LoadSongInformationByIdAsync(musicFileId);
        _songViewModel = result;
        SelectedGenre = Genres
            .FirstOrDefault(x => x.GenreId == result.Genre.GenreId)?.GenreId.ToString() ?? "";

        ArtistSuggestions = _artists.Select(ObjectUtilities.CopyObject).ToList();
        ;

        SelectedArtist = _artists
            .First(x => x.ArtistId == result.Artist.ArtistId);
        await SelectArtist(SelectedArtist);
        HasChanges = false;
    }
    
    public async Task LoadGenres()
    {
        // Load the song from the database
        // and populate the form fields
        var result = await _musicSearchService.GetAllGenresAsync();
        Genres = result;
    }
}