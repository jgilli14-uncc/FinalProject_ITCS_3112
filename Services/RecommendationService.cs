namespace FinalProject3112.Services;
using Spectre.Console;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class RecommendationService : IRecommendationService
{
    private IGameRepository gameRepository;

    public RecommendationService(IGameRepository gameRepository)
    {
        this.gameRepository = gameRepository;
    }
    public void GetRecommendations(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Get Recommendations[/]");
        Console.WriteLine();

        // pick a strategy
        Console.WriteLine("Select a recommendation strategy:");
        Console.WriteLine("1. By Genre");
        Console.WriteLine("2. By Publisher");
        Console.WriteLine("3. Default");
        Console.Write("Choice: ");
        string strategyInput = Console.ReadLine();

        RecommendationSystem strategy;

        switch (strategyInput)
        {
            case "1": strategy = new RecommendationByGenre(); break;
            case "2": strategy = new RecommendationByPublisher(); break;
            case "3": strategy = new RecommendationByDefault(); break;
            default:
                AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
                Console.ReadLine();
                return;
        }

        // generate recommendations
        List<Game> recommendations = strategy.filter(gameRepository.GetAllGames(), currentUser);

        // filter out games already in the user's library
        List<Game> filtered = new List<Game>();

        for (int i = 0; i < recommendations.Count; i++)
        {
            bool alreadyOwned = false;
            for (int j = 0; j < currentUser.gameList.Count; j++)
            {
                if (currentUser.gameList[j].gameId == recommendations[i].gameId)
                    alreadyOwned = true;
            }
            if (!alreadyOwned)
                filtered.Add(recommendations[i]);
        }

        if (filtered.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No recommendations found.[/]");
            Console.ReadLine();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Recommended games:");
        Console.WriteLine();

        for (int i = 0; i < filtered.Count; i++)
            Console.WriteLine($"{i + 1}. {filtered[i].name} | {filtered[i].genre} | {filtered[i].platform}");

        Console.WriteLine();
        Console.WriteLine("Enter the number of a game to add it to your library.");
        Console.WriteLine("Press Enter with no input to go back.");

        // let user keep picking games to add
        while (true)
        {
            Console.Write("Choice: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            int choice;
            if (!int.TryParse(input, out choice) || choice < 1 || choice > filtered.Count)
            {
                AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
                continue;
            }

            Game selectedGame = filtered[choice - 1];
            currentUser.gameList.Add(selectedGame);
            GameRepository.getInstance().AddGame(selectedGame);
            selectedGame.status = PlayStatus.Unplayed;

            AnsiConsole.MarkupLine($"[green]'{selectedGame.name}' added to your library![/]");
        }
    }
}