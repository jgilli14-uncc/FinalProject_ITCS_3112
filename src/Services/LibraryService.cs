namespace FinalProject3112.Services;
using Spectre.Console;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class LibraryService : ILibraryService
{
    private IGameRepository gameRepository;

    public LibraryService(IGameRepository gameRepository)
    {
        this.gameRepository = gameRepository;
    }

    public void ViewLibrary(BasicUser currentUser)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[green]{currentUser.username}'s Game Library[/]");
                Console.WriteLine();

                if (currentUser.gameList.Count == 0)
                {
                    AnsiConsole.WriteLine("[yellow]Your library is empty.[/]");
                }
                else
                {
                    for (int i = 0; i < currentUser.gameList.Count; i++)
                    {
                        Game g = currentUser.gameList[i];
                        Console.WriteLine($"{i + 1}. {g.name} | {g.genre} | {g.platform}");
                    }
                }

                Console.WriteLine();
                AnsiConsole.WriteLine("Press Enter to go back.");
                Console.ReadLine();
            }

            // add game to library and repository
    public void AddGameToLibrary(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Add Game to Library[/]");
        Console.WriteLine();

        Console.Write("Game Name: ");
        string name = Console.ReadLine();
        Console.Write("Genre: ");
        string genre = Console.ReadLine();
        Console.Write("Publisher: ");
        string publisher = Console.ReadLine();
        Console.Write("Date Released: ");
        string dateReleased = Console.ReadLine();
        Console.Write("Platform: ");
        string platform = Console.ReadLine();

        Game newGame = new Game(name, genre, publisher, dateReleased, platform);
        GameRepository.getInstance().AddGame(newGame);
        currentUser.gameList.Add(newGame);
        
        AnsiConsole.MarkupLine("[green]Game added to library![/]");
        Console.ReadLine();
    }

    // remove game for library and repository
    public void RemoveGameFromLibrary(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Remove Game from Library[/]");
        Console.WriteLine();

        if (currentUser.gameList.Count == 0)
        {
            AnsiConsole.WriteLine("[yellow]Your library is empty.[/]");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < currentUser.gameList.Count; i++)
        {
            Game g = currentUser.gameList[i];
            Console.WriteLine($"{i + 1}. {g.name} | {g.genre} | {g.platform}");
        }

        Console.WriteLine();
        
        // convert user input to int
        Console.Write("Enter the number of the game to remove: ");
        string input = Console.ReadLine();
        int choice;

        // validate input
        if (!int.TryParse(input, out choice))
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        if (choice < 1 || choice > currentUser.gameList.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        //remove game from user's library and game repository
        Game removedGame = currentUser.gameList[choice - 1];
        currentUser.gameList.RemoveAt(choice - 1);
        GameRepository.getInstance().RemoveGame(removedGame);
        AnsiConsole.MarkupLine($"[green]{removedGame.name} removed from library![/]");
        Console.ReadLine();
    }
    // search RAWG and let the user pick a game to add, instead of typing details manually
    public void AddGameFromRawg(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Add Game from RAWG[/]");
        Console.WriteLine();

        Console.Write("Search for a game: ");
        string searchTerm = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            AnsiConsole.MarkupLine("[red]Search term cannot be empty.[/]");
            Console.ReadLine();
            return;
        }

        IRawgImportService rawgService = new RawgImportService(RawgConfig.ApiKey);

        AnsiConsole.MarkupLine("[yellow]Searching RAWG...[/]");
        List<Game> results = rawgService.SearchGamesAsync(searchTerm).GetAwaiter().GetResult();

        if (results.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No results found.[/]");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        AnsiConsole.MarkupLine("[green]Search Results[/]");
        Console.WriteLine();

        for (int i = 0; i < results.Count; i++)
        {
            Game g = results[i];
            Console.WriteLine($"{i + 1}. {g.name} | {g.genre} | {g.dateReleased} | {g.platform}");
        }

        Console.WriteLine();
        Console.Write("Enter the number of the game to add (or press Enter to cancel): ");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            return;

        int choice;
        if (!int.TryParse(input, out choice) || choice < 1 || choice > results.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        Game selectedGame = results[choice - 1];
        GameRepository.getInstance().AddGame(selectedGame);
        currentUser.gameList.Add(selectedGame);

        AnsiConsole.MarkupLine($"[green]'{selectedGame.name}' added to your library![/]");
        Console.ReadLine();
    }
}