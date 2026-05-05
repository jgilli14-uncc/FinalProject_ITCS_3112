namespace FinalProject3112.Services;
using System.Text.Json;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

//change to fileService

//make an interface for this class

public class FileLoad
{
    private static FileLoad instance;
    private static string gamesPath = "data/games.json";
    private static string usersPath = "data/users.json";
    private static string ratingsPath = "data/ratings.json";


    private FileLoad() { }

    public static FileLoad getInstance()
    {
        if (instance == null)
            instance = new FileLoad();
        return instance;
    }

    public void LoadGamesFile()
    {
        if (!File.Exists(gamesPath)) return;

        string json = File.ReadAllText(gamesPath);
        List<Game> games = JsonSerializer.Deserialize<List<Game>>(json);

        if (games == null) return;

        for (int i = 0; i < games.Count; i++)
            GameRepository.getInstance().gameDatabase.Add(games[i]);

        GameRepository.getInstance().gameIdCounter = games.Count + 1;
    }

    public void LoadUsersFile()
    {
        if (!File.Exists(usersPath)) return;

        string json = File.ReadAllText(usersPath);
        List<BasicUser> users = JsonSerializer.Deserialize<List<BasicUser>>(json);

        if (users == null) return;

        for (int i = 0; i < users.Count; i++)
            UserRepository.getInstance().userDatabase.Add(users[i]);

        UserRepository.getInstance().userIdCounter = users.Count + 1;
    }

    public void LoadRatingsFile()
    {
        if (!File.Exists(ratingsPath)) return;

        string json = File.ReadAllText(ratingsPath);
        List<Rating> ratings = JsonSerializer.Deserialize<List<Rating>>(json);

        if (ratings == null) return;

        for (int i = 0; i < ratings.Count; i++)
            RatingRepository.getInstance().ratingDatabase.Add(ratings[i]);
    }

    public void SaveGamesFile()
    {
        string json = JsonSerializer.Serialize(GameRepository.getInstance().gameDatabase);
        File.WriteAllText(gamesPath, json);
    }

    public void SaveUsersFile()
    {
        List<BasicUser> users = new List<BasicUser>();

        for (int i = 0; i < UserRepository.getInstance().userDatabase.Count; i++)
        {
            BasicUser u = UserRepository.getInstance().userDatabase[i] as BasicUser;
            if (u != null)
                users.Add(u);
        }
        
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText(usersPath, json);
    }
    //D in SOLID

    public void SaveRatingsFile()
    {
        string json = JsonSerializer.Serialize(RatingRepository.getInstance().ratingDatabase);
        File.WriteAllText(ratingsPath, json);
    }
}