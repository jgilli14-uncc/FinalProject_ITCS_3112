namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IRecommendationService
{
    void GetRecommendations(BasicUser currentUser);
}