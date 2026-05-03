namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IRecommendationSystem
{
    public List<Game> GenerateRecommendations(int userID);
}