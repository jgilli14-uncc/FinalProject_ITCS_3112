namespace FinalProject3112.Services;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;

public class RecommendationByDefault : RecommendationSystem
{
    public override List<Game> filter(List<Game> games, BasicUser user)
    {
        return games;
    }
}