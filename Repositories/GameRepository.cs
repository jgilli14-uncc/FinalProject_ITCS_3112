public class GameRepository : IGameRepository
{
    private static GameRepository instance;
    public List<Game> gameDatabase { get; set; }

    private GameRepository()
    {
        gameDatabase = new List<Game>();
    }
    private static GameRepository getInstance()
    {
        if (instance == null)
        {
            instance = new GameRepository();
        }
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
}