namespace FinalProject3112.Services;
using Spectre.Console;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class RatingService : IRatingService
{

    private IRatingRepository ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        this.ratingRepository = ratingRepository;
    }
    
    public void ViewRatings(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]My Ratings[/]");
        Console.WriteLine();

        List<Rating> ratings = RatingRepository.getInstance().GetRatingsByUser(currentUser.userID);

        int count = 1;
        for (int i = 0; i < ratings.Count; i++)
        {
            // find the game name for this rating
            string gameName = "";
            for (int j = 0; j < currentUser.gameList.Count; j++)
            {
                if (currentUser.gameList[j].gameId == ratings[i].gameID)
                    gameName = currentUser.gameList[j].name;
            }
            Console.WriteLine($"{count}. {gameName} | Score: {ratings[i].ratingNum} | {ratings[i].ratingText}");
            count++;
        }

        if (count == 1)
            AnsiConsole.MarkupLine("[yellow]You haven't rated any games yet.[/]");

        Console.WriteLine();
        Console.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }

    public void RateGame(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Rate a Game[/]");
        Console.WriteLine();

        // only show completed games
        List<Game> completedGames = new List<Game>();

        for (int i = 0; i < currentUser.gameList.Count; i++)
        {
            if (currentUser.gameList[i].status == PlayStatus.Completed)
                completedGames.Add(currentUser.gameList[i]);
        }

        if (completedGames.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]You have no completed games to rate.[/]");
            Console.ReadLine();
            return;
        }

        // show completed games and filter out already rated ones
        List<Game> unratedGames = new List<Game>();

        for (int i = 0; i < completedGames.Count; i++)
        {
            Rating existing = RatingRepository.getInstance().GetRating(completedGames[i].gameId, currentUser.userID);
            if (existing == null)
                unratedGames.Add(completedGames[i]);
        }

        if (unratedGames.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]You have already rated all your completed games.[/]");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < unratedGames.Count; i++)
            Console.WriteLine($"{i + 1}. {unratedGames[i].name}");

        Console.WriteLine();
        Console.Write("Enter the number of the game to rate: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 1 || choice > unratedGames.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        Game selectedGame = unratedGames[choice - 1];

        double score = 0;
        bool validScore = false;
        while (!validScore)
        {
            Console.Write("Score (1.0 - 10.0): ");
            if (double.TryParse(Console.ReadLine(), out score) && score >= 1.0 && score <= 10.0)
                validScore = true;
            else
                AnsiConsole.MarkupLine("[red]Please enter a number between 1.0 and 10.0.[/]");
        }

        Console.Write("Short review (or leave blank): ");
        string review = Console.ReadLine();

        Rating rating = new Rating(selectedGame.gameId, currentUser.userID, score, review);
        RatingRepository.getInstance().AddRating(rating);
        currentUser.ratingList.Add(rating);

        // update the game's average rating
        selectedGame.averageRating = score;

        AnsiConsole.MarkupLine($"[green]{selectedGame.name} rated {score}/10![/]");
        Console.WriteLine();
        Console.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }

    public void EditRating(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Edit a Rating[/]");
        Console.WriteLine();

        List<Rating> ratings = RatingRepository.getInstance().GetRatingsByUser(currentUser.userID);

        if (ratings.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]You have no ratings to edit.[/]");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < ratings.Count; i++)
        {
            string gameName = "";
            for (int j = 0; j < currentUser.gameList.Count; j++)
            {
                if (currentUser.gameList[j].gameId == ratings[i].gameID)
                    gameName = currentUser.gameList[j].name;
            }
            Console.WriteLine($"{i + 1}. {gameName} | Score: {ratings[i].ratingNum} | {ratings[i].ratingText}");
        }

        Console.WriteLine();
        Console.Write("Enter the number of the rating to edit: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 1 || choice > ratings.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        Rating selectedRating = ratings[choice - 1];

        double score = 0;
        bool validScore = false;
        while (!validScore)
        {
            Console.Write($"New score ({selectedRating.ratingNum}): ");
            if (double.TryParse(Console.ReadLine(), out score) && score >= 1.0 && score <= 10.0)
                validScore = true;
            else
                AnsiConsole.MarkupLine("[red]Please enter a number between 1.0 and 10.0.[/]");
        }

        Console.Write($"New review ({selectedRating.ratingText}): ");
        string review = Console.ReadLine();

        selectedRating.ratingNum = score;
        if (!string.IsNullOrWhiteSpace(review))
            selectedRating.ratingText = review;

        AnsiConsole.MarkupLine("[green]Rating updated![/]");
        Console.WriteLine();
        Console.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }

    public void RemoveRating(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Remove a Rating[/]");
        Console.WriteLine();

        List<Rating> ratings = RatingRepository.getInstance().GetRatingsByUser(currentUser.userID);

        if (ratings.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]You have no ratings to remove.[/]");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < ratings.Count; i++)
        {
            string gameName = "";
            for (int j = 0; j < currentUser.gameList.Count; j++)
            {
                if (currentUser.gameList[j].gameId == ratings[i].gameID)
                    gameName = currentUser.gameList[j].name;
            }
            Console.WriteLine($"{i + 1}. {gameName} | Score: {ratings[i].ratingNum}");
        }

        Console.WriteLine();
        Console.Write("Enter the number of the rating to remove: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 1 || choice > ratings.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        Rating selectedRating = ratings[choice - 1];
        RatingRepository.getInstance().RemoveRating(selectedRating);
        currentUser.ratingList.Remove(selectedRating);

        AnsiConsole.MarkupLine("[green]Rating removed![/]");
        Console.WriteLine();
        Console.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }
}