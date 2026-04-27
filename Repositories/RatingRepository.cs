public class RatingRepository : IRatingRepository
{
    private static RatingRepository instance;
    public List<Rating> ratingDatabase { get; set; }

    private RatingRepository()
    {
        ratingDatabase = new List<Rating>();
    }

    private static RatingRepository getInstance()
    {
        if (instance == null)
        {
            instance = new RatingRepository();
        }
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
}