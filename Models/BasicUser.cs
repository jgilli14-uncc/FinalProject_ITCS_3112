public class BasicUser
{
    public int userID { get; set; }
    public string username { get; set; }
    private string password { get; set; }
    public List<Game> gameList { get; set; }
    public List<Rating> ratingList { get; set; }

    public BasicUser (string username, string password)
    {
        this.username = username;
        this.password = password;
        gameList = new List<Game>();
        ratingList = new List<Rating>();
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
        Console.WriteLine($"Games in Library: {gameList.Count}");
        Console.WriteLine($"Ratings Given: {ratingList.Count}");
    }
}