namespace FinalProject3112.Services;
using System.Text.Json;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class RatingFileService : FileService
{
    protected override string GetFilePath()
    {
        return "src/data/ratings.json";
    }

    protected override void Deserialize(string json)
    {
        List<Rating> ratings = JsonSerializer.Deserialize<List<Rating>>(json);
        if (ratings == null) return;
        RatingRepository.getInstance().ratingDatabase.Clear();
        for (int i = 0; i < ratings.Count; i++)
            RatingRepository.getInstance().ratingDatabase.Add(ratings[i]);
    }

    protected override string Serialize()
    {
        return JsonSerializer.Serialize(RatingRepository.getInstance().ratingDatabase);
    }
}