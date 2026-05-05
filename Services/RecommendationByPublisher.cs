namespace FinalProject3112.Services;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;

public class RecommendationByPublisher : RecommendationSystem
{
    public override List<Game> filter(List<Game> games, BasicUser user)
    {
        if (user.gameList.Count == 0)
        {
            return games;
        }

        // Get the most popular publisher
        string favoritePublisher = "";
        int highestCount = 0;

        for (int i = 0; i < user.gameList.Count; i++)
        {
            int count = 0;
            string publisher = user.gameList[i].publisher;

            for (int j = 0; j < user.gameList.Count; j++)
            {
                if (user.gameList[j].publisher == publisher)
                    count++;
            }

            if (count > highestCount)
            {
                highestCount = count;
                favoritePublisher = publisher;
            }
        }
        // Filter games by the most popular publisher
        List<Game> recommendedGames = new List<Game>();

        for (int i = 0; i < games.Count; i++)
        {
            if (games[i].publisher == favoritePublisher)
                recommendedGames.Add(games[i]);
        }

        return recommendedGames;
    }
}