namespace FinalProject3112.Services;
using Spectre.Console;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class AccountService : IAccountService
{
    private IUserRepository userRepository;

    public AccountService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public void ChangeUsername(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[cyan] Change Username [/]");
        Console.WriteLine();

        Console.Write("New username: ");
        string newName = Console.ReadLine();

        // update locally and in repository
        UserRepository.getInstance().ModifyUserName(currentUser.userID, newName);
        currentUser.username = newName;

        AnsiConsole.MarkupLine("[green]Username updated![/]");
        Console.ReadLine();
    }
    public void ChangePassword(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[cyan] Change Password [/]");
        Console.WriteLine();

        Console.Write("New password: ");
        string newPassword = Console.ReadLine();
        Console.Write("Confirm new password: ");
        string confirm = Console.ReadLine();

        if (newPassword != confirm)
        {
            AnsiConsole.MarkupLine("[red]Passwords do not match.[/]");
            Console.ReadLine();
            return;
        }

        // update locally and in repository
        UserRepository.getInstance().ModifyUserPassword(currentUser.userID, newPassword);
        currentUser.SetPassword(newPassword);

        AnsiConsole.MarkupLine("[green]Password updated![/]");
        Console.ReadLine();
    }
    
    public bool DeleteAccount(BasicUser currentUser)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[red] Delete Account [/]");
        Console.WriteLine();

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Are you sure you want to delete your account? This action cannot be undone.")
                .AddChoices("Yes", "No"));

        if (confirm == "Yes")
        {
            UserRepository.getInstance().RemoveUser(currentUser);
            AnsiConsole.MarkupLine("[green]Account deleted.[/]");
            Console.ReadLine();
            return true;
        }

        return false;
    }
}