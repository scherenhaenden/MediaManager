using Newtonsoft.Json;

namespace BeOneSender.BusinessLogic.Services.Bootstrap;

public class RawListSongs
{
    [JsonProperty("Song Clean")] public string SongClean { get; set; }

    [JsonProperty("ARTIST CLEAN")] public string ARTISTCLEAN { get; set; }

    [JsonProperty("Release Year")] public string ReleaseYear { get; set; }

    public string COMBINED { get; set; }

    [JsonProperty("First?")] public string First { get; set; }

    [JsonProperty("Year?")] public string Year { get; set; }

    public string PlayCount { get; set; }

    [JsonProperty("F*G")] public string FG { get; set; }
}