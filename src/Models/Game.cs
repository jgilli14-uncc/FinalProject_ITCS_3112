namespace FinalProject3112.Models;

public class Game
{
    public int gameId { get; set; }
    public string name { get; set; }
    public string genre { get; set; }
    public string publisher { get; set; }
    public string dateReleased { get; set; }
    public string platform { get; set; }
    public double averageRating { get; set; }
    public PlayStatus status { get; set; }

    public Game()
    {
        
    }
    public Game(string name, string genre, string publisher, string dateReleased, string platform)
    {
        this.name = name;
        this.genre = genre;
        this.publisher = publisher;
        this.dateReleased = dateReleased;
        this.platform = platform;
        this.status = PlayStatus.Unplayed;
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