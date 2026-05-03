namespace FinalProject3112.Repositories;
using FinalProject3112.Models;
using FinalProject3112.Interfaces;

public class GameRepository : IGameRepository
{
    public static GameRepository instance;
    public List<Game> gameDatabase { get; set; }

    private GameRepository()
    {
        gameDatabase = new List<Game>();
    }
    public static GameRepository getInstance()
    {
        if (instance == null)
            instance = new GameRepository();
        return instance;
    }
    public void AddGame(Game game)
    {
        gameDatabase.Add(game);
    }
    public void RemoveGame(Game game)
    {
        gameDatabase.Remove(game);
    }
    public void UpdateGame(Game game)
    {
        var existingGame = GetGameById(game.gameId);
        if (existingGame != null)
        {
            existingGame.name = game.name;
            existingGame.genre = game.genre;
            existingGame.publisher = game.publisher;
            existingGame.dateReleased = game.dateReleased;
            existingGame.platform = game.platform;
            existingGame.averageRating = game.averageRating;
        }
    }
    public Game GetGameById(int id)
    {
        return gameDatabase.FirstOrDefault(g => g.gameId == id);
    }
    public List<Game> GetAllGames()
    {
        return gameDatabase;
    }
}