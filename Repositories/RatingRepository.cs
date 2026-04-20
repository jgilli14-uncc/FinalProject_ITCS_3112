using FinalProject3112.Interfaces;

public class RatingRepository : IRatingRepository
{
    private RatingRepository instance;
    private List<Rating> ratingDatabase;

    private void RatingRepository()
    {
        
    }

    public RatingRepository getInstance()
    {
        return instance;
    }

    public AddRating(int gameID, int userID, double ratingNum, string ratingText)
    {
        
    }

    public RemoveRating(int gameID, int userID, int vUserID)
    {
        
    }
}