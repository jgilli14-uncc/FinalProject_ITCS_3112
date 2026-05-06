namespace FinalProject3112.Models;

public class BasicUser : User
{
    public List<Game> gameList { get; set; }
    public List<Rating> ratingList { get; set; }

    public BasicUser() : base("", "")
    {
        gameList = new List<Game>();
        ratingList = new List<Rating>();
    }
    public BasicUser(string username, string password) : base(username, password)
    {
        gameList = new List<Game>();
        ratingList = new List<Rating>();
    }
    public new void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
        Console.WriteLine($"Games in Library: {gameList.Count}");
        Console.WriteLine($"Ratings Given: {ratingList.Count}");
    }
}