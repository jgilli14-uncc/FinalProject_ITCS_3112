namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IRawgImportService
{
    // Searches RAWG for games matching the given name. Returns up to 'maxResults' matches.
    Task<List<Game>> SearchGamesAsync(string searchTerm, int maxResults = 10);

    // Fetches full detail (including publisher) for a specific RAWG game id and
    // returns a fully-populated Game ready to add to the repository.
    Task<Game> GetGameDetailAsync(int rawgGameId);
}