namespace FinalProject3112.Interfaces;

public interface IRatingRepository
{
    public AddRating(int gameID, int userID, double ratingNum, string ratingText);
    public RemoveRating(int gameID, int userID, int vUserID);
}