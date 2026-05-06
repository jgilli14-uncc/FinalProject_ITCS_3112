namespace FinalProject3112.Services;
using Spectre.Console;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class BacklogService : IBacklogService
{
    public void ViewBacklog(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]My Backlog[/]");
        Console.WriteLine();


        int count = 1;
        for (int i = 0; i < currentUser.gameList.Count; i++)
        {
            if (currentUser.gameList[i].status != PlayStatus.Completed)
            {
                Game g = currentUser.gameList[i];
                Console.WriteLine($"{count}. {g.name} | {g.genre} | {g.platform} | Status: {g.status}");
                count++;
            }
        }

        if (count == 1)
        {
            AnsiConsole.WriteLine("[yellow]Your backlog is empty.[/]");
        }

        Console.WriteLine();
        AnsiConsole.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }

    public void UpdateStatus(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Update Game Status[/]");
        Console.WriteLine();

        // build list of backlog games
        List<Game> backlog = new List<Game>();

        for (int i = 0; i < currentUser.gameList.Count; i++)
        {
            if (currentUser.gameList[i].status != PlayStatus.Completed)
                backlog.Add(currentUser.gameList[i]);
        }

        if (backlog.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Your backlog is empty.[/]");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < backlog.Count; i++)
            Console.WriteLine($"{i + 1}. {backlog[i].name} | {backlog[i].status}");

        Console.WriteLine();
        Console.Write("Enter the number of the game to update: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 1 || choice > backlog.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
            Console.ReadLine();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Select new status:");
        Console.WriteLine("1. Unplayed");
        Console.WriteLine("2. InProgress");
        Console.WriteLine("3. Completed");
        Console.Write("Choice: ");
        string statusInput = Console.ReadLine();

        PlayStatus newStatus;

        switch (statusInput)
        {
            case "1": newStatus = PlayStatus.Unplayed; break;
            case "2": newStatus = PlayStatus.InProgress; break;
            case "3": newStatus = PlayStatus.Completed; break;
            default:
                AnsiConsole.MarkupLine("[red]Invalid choice.[/]");
                Console.ReadLine();
                return;
        }

        backlog[choice - 1].status = newStatus;

        AnsiConsole.MarkupLine($"[green]{backlog[choice - 1].name} updated to {newStatus}![/]");
        Console.WriteLine();
        Console.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }
}