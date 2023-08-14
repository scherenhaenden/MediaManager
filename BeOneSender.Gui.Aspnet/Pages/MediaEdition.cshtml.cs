using System.Web;
using BeOneSender.Gui.Aspnet.Data;
using BeOneSender.Gui.Aspnet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeOneSender.Gui.Aspnet.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class MediaEditionModel: PageModel
{
    private readonly IMusicSearchService _musicSearchService;

    public MediaEditionModel(IMusicSearchService musicSearchService, IMusicEditService MusicEditService)
    {
        _musicSearchService = musicSearchService;
    }
    
    
    // onInitializedAsync
    public async Task OnGetAsync()
    {
        var uri = new Uri(NavigationManager.Uri);
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
}