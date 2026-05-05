namespace FinalProject3112.Services;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;

public class RecommendationByGenre : RecommendationSystem
{
    public override List<Game> filter(List<Game> games, BasicUser user)
    {
        if (user.gameList.Count == 0)
        {
            return games;
        }

        // Get the most played genre
        string favoriteGenre = "";
        int highestCount = 0;

        for (int i = 0; i < user.gameList.Count; i++)
        {
            int count = 0;
            string genre = user.gameList[i].genre;

            for (int j = 0; j < user.gameList.Count; j++)
            {
                if (user.gameList[j].genre == genre)
                    count++;
            }

            if (count > highestCount)
            {
                highestCount = count;
                favoriteGenre = genre;
            }
        }
        // Filter games by the most played genre
        List<Game> recommendedGames = new List<Game>();

        for (int i = 0; i < games.Count; i++)
        {
            if (games[i].genre == favoriteGenre)
                recommendedGames.Add(games[i]);
        }

        return recommendedGames;
    }
}