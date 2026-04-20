using FinalProject3112.Interfaces;

public class GameRepository : IGameRepository
{
    private GameRepository instance;
    private List<Game> gameDatabase;

    private void GameRepository()
    {
        
    }

    public GameRepository getInstance()
    {
        return instance;
    }
    
    public AddGame(int vUserID)
    {
        
    }

    public RemoveGame(int gameID, int vUserID)
    {
        
    }
}