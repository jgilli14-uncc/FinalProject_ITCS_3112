namespace FinalProject3112.Services;
using System.Text.Json;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class GamesFileService : FileService
{
    protected override string GetFilePath()
    {
        return "docs/games.json";
    }

    protected override void Deserialize(string json)
    {
        List<Game> games = JsonSerializer.Deserialize<List<Game>>(json);
        if (games == null) return;
        GameRepository.getInstance().gameDatabase.Clear();
        for (int i = 0; i < games.Count; i++)
            GameRepository.getInstance().gameDatabase.Add(games[i]);
        GameRepository.getInstance().gameIdCounter = games.Count + 1;
    }

    protected override string Serialize()
    {
        return JsonSerializer.Serialize(GameRepository.getInstance().gameDatabase);
    }
}