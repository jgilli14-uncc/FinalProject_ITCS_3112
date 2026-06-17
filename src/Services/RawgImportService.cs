namespace FinalProject3112.Services;

using System.Net.Http;
using System.Text.Json;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.External;

public class RawgImportService : IRawgImportService
{
    private readonly HttpClient httpClient;
    private readonly string apiKey;
    private const string BaseUrl = "https://api.rawg.io/api";

    // JsonSerializerOptions reused across calls - RAWG's JSON uses snake_case which
    // we've already mapped explicitly via [JsonPropertyName], so default options are fine.
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public RawgImportService(string apiKey)
    {
        this.apiKey = apiKey;
        this.httpClient = new HttpClient();
    }

    public async Task<List<Game>> SearchGamesAsync(string searchTerm, int maxResults = 10)
    {
        var games = new List<Game>();

        if (string.IsNullOrWhiteSpace(searchTerm))
            return games;

        string url = $"{BaseUrl}/games?key={apiKey}&search={Uri.EscapeDataString(searchTerm)}&page_size={maxResults}";

        string json;
        try
        {
            json = await httpClient.GetStringAsync(url);
        }
        catch (HttpRequestException ex)
        {
            // Network or API failure - surface as empty list so the menu can show
            // "no results / try again" rather than crashing the whole app.
            Console.WriteLine($"[RAWG] Request failed: {ex.Message}");
            return games;
        }

        RawgSearchResponse response = JsonSerializer.Deserialize<RawgSearchResponse>(json, jsonOptions);

        if (response == null || response.Results == null)
            return games;

        foreach (RawgGameSummary result in response.Results)
        {
            games.Add(MapSummaryToGame(result));
        }

        return games;
    }

    public async Task<Game> GetGameDetailAsync(int rawgGameId)
    {
        string url = $"{BaseUrl}/games/{rawgGameId}?key={apiKey}";

        string json;
        try
        {
            json = await httpClient.GetStringAsync(url);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"[RAWG] Request failed: {ex.Message}");
            return null;
        }

        RawgGameDetail detail = JsonSerializer.Deserialize<RawgGameDetail>(json, jsonOptions);

        if (detail == null)
            return null;

        return MapDetailToGame(detail);
    }

    private Game MapSummaryToGame(RawgGameSummary summary)
    {
        Game game = new Game
        {
            name = summary.Name,
            genre = summary.Genres.Count > 0 ? summary.Genres[0].Name : "Unknown",
            publisher = "Unknown", // not available on the search endpoint, filled in via GetGameDetailAsync
            dateReleased = summary.Released ?? "Unknown",
            platform = summary.Platforms.Count > 0 ? summary.Platforms[0].Platform.Name : "Unknown",
        };
        game.status = PlayStatus.Unplayed;
        return game;
    }

    private Game MapDetailToGame(RawgGameDetail detail)
    {
        Game game = new Game
        {
            name = detail.Name,
            genre = detail.Genres.Count > 0 ? detail.Genres[0].Name : "Unknown",
            publisher = detail.Publishers.Count > 0 ? detail.Publishers[0].Name : "Unknown",
            dateReleased = detail.Released ?? "Unknown",
            platform = detail.Platforms.Count > 0 ? detail.Platforms[0].Platform.Name : "Unknown",
        };
        game.status = PlayStatus.Unplayed;
        return game;
    }
}