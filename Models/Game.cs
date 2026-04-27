public class Game
{
    public int gameId { get; set; }
    public string name { get; set; }
    private string genre { get; set; }
    private string publisher { get; set; }
    private string dateReleased { get; set; }
    public string platform { get; set; }
    public double averageRating { get; set; }

    public Game(string name, string genre, string publisher, string dateReleased, string platform)
    {
        this.name = name;
        this.genre = genre;
        this.publisher = publisher;
        this.dateReleased = dateReleased;
        this.platform = platform;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Genre: {genre}");
        Console.WriteLine($"Publisher: {publisher}");
        Console.WriteLine($"Date Released: {dateReleased}");
        Console.WriteLine($"Platform: {platform}");
        Console.WriteLine($"Average Rating: {averageRating}");
    }
}