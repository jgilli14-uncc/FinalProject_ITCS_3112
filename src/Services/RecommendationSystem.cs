namespace FinalProject3112.Services;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class RecommendationSystem : IRecommendationSystem
{
    public virtual List<Game> filter(List<Game> games, BasicUser user)
    {
        return games;
    }

    public List<Game> GenerateRecommendations(int userID)
    {
        User user = UserRepository.getInstance().getUserbyID(userID);
        BasicUser basicUser = user as BasicUser;
        List<Game> games = GameRepository.getInstance().GetAllGames();
        return filter(games, basicUser);
    }
}