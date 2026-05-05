namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IRatingRepository
{
    public void AddRating(Rating rating);
    public void RemoveRating(Rating rating);
    Rating GetRating(int gameId, int userID);
    List<Rating> GetRatingsByUser(int userID);
}