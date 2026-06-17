namespace FinalProject3112.External;

using System.Text.Json.Serialization;

// Top-level response from GET /api/games?search=...
public class RawgSearchResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("results")]
    public List<RawgGameSummary> Results { get; set; } = new();
}

// One entry in the search results list
public class RawgGameSummary
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("released")]
    public string Released { get; set; }

    [JsonPropertyName("genres")]
    public List<RawgGenre> Genres { get; set; } = new();

    [JsonPropertyName("platforms")]
    public List<RawgPlatformWrapper> Platforms { get; set; } = new();
}

public class RawgGenre
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}

// RAWG nests the actual platform object inside a wrapper: { "platform": { "name": "PC" } }
public class RawgPlatformWrapper
{
    [JsonPropertyName("platform")]
    public RawgPlatform Platform { get; set; } = new();
}

public class RawgPlatform
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}

// Response from GET /api/games/{id} - the detail endpoint, used for publisher info
public class RawgGameDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("released")]
    public string Released { get; set; }

    [JsonPropertyName("genres")]
    public List<RawgGenre> Genres { get; set; } = new();

    [JsonPropertyName("platforms")]
    public List<RawgPlatformWrapper> Platforms { get; set; } = new();

    [JsonPropertyName("publishers")]
    public List<RawgPublisher> Publishers { get; set; } = new();
}

public class RawgPublisher
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}