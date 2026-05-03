namespace FinalProject3112.Models;

public class Rating
{
    public int gameID { get; set; }
    public int userID { get; set; }
    public double ratingNum { get; set; }
    public string ratingText { get; set; }

    public Rating(int gameID, int userID, double ratingNum, string ratingText)
    {
        this.gameID = gameID;
        this.userID = userID;
        this.ratingNum = ratingNum;
        this.ratingText = ratingText;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Game ID: {gameID}");
        Console.WriteLine($"User ID: {userID}");
        Console.WriteLine($"Rating Number: {ratingNum}");
        Console.WriteLine($"Rating Text: {ratingText}");
    }
}