namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IGameRepository
{
    public void AddGame(Game game);
    public void RemoveGame(Game game);

    public void UpdateGame(Game game);
    public Game GetGameById(int id);
    public List<Game> GetAllGames();

}