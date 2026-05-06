namespace FinalProject3112.Repositories;
using FinalProject3112.Models;
using FinalProject3112.Interfaces;

public class RatingRepository : IRatingRepository
{
    private static RatingRepository instance;
    public List<Rating> ratingDatabase { get; set; }


    private RatingRepository()
    {
        ratingDatabase = new List<Rating>();
    }
    public static RatingRepository getInstance()
    {
        if (instance == null)
            instance = new RatingRepository();
        return instance;
    }
    public void AddRating(Rating rating)
    {
        ratingDatabase.Add(rating);
    }
    public void RemoveRating(Rating rating)
    {
        ratingDatabase.Remove(rating);
    }
    public Rating GetRating(int gameId, int userID)
    {
        for (int i = 0; i < ratingDatabase.Count; i++)
        {
            if (ratingDatabase[i].gameID == gameId && ratingDatabase[i].userID == userID)
                return ratingDatabase[i];
        }
        return null;
    }

    public List<Rating> GetRatingsByUser(int userID)
    {
        List<Rating> userRatings = new List<Rating>();

        for (int i = 0; i < ratingDatabase.Count; i++)
        {
            if (ratingDatabase[i].userID == userID)
                userRatings.Add(ratingDatabase[i]);
        }

        return userRatings;
    }
}