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
}