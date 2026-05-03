namespace FinalProject3112.Repositories;
using FinalProject3112.Models;
using FinalProject3112.Interfaces;

public class RatingRepository : IRatingRepository
{
    public static RatingRepository instance;
    public List<Rating> ratingDatabase { get; set; }

    private RatingRepository()
    {
        ratingDatabase = new List<Rating>();
    }
    private static RatingRepository getInstance()
    {
        if (instance == null)
            instance = new RatingRepository();
        return instance;
    }
    public void AddRating(Rating rating) => ratingDatabase.Add(rating);
    public void RemoveRating(Rating rating) => ratingDatabase.Remove(rating);
}