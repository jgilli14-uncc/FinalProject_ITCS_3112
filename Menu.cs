using Spectre.Console;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

namespace FinalProject3112.Menus
{
    public class Menu
    {
        private BasicUser currentUser = null;
        private SuperUser currentAdmin = null;
        private GameRepository gameRepo = GameRepository.getInstance();

        public void Start()
        {
            while (true)
            {
                if (currentUser == null && currentAdmin == null)
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Welcome to the Game Recommendation System! Please Login or Register:")
                            .AddChoices("Login", "Register", "Exit"));

                    switch (choice)
                    {
                        case "Login":    Login(); break;
                        case "Register": Register(); break;
                        case "Exit":     return;
                    }
                }
                else
                {
                    if (currentAdmin is SuperUser)
                        AdminMenu();
                    else
                        MainMenu();

                    currentUser = null;
                    currentAdmin = null;
                }
            }
        }

        // user login
        private void Login()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]=== Login ===[/]");
            Console.WriteLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            currentUser = new BasicUser(username, password);
        }

        // new user registration
        private void Register()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]=== Register ===[/]");
            Console.WriteLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Confirm Password: ");
            string confirm = Console.ReadLine();

            if (password != confirm)
            {
                AnsiConsole.MarkupLine("[red]Passwords do not match.[/]");
                Console.ReadLine();
                return;
            }

            AnsiConsole.MarkupLine("[green]Account created successfully![/]");
            Console.ReadLine();

            currentUser = new BasicUser(username, password) { userID = 1 };
        }
        private void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[green]Welcome, {currentUser.username}![/]");
                Console.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("My Library", "My Backlog", "Ratings", "Recommendations", "Account", "Logout"));

                switch (choice)
                {
                    case "My Library":      LibraryMenu(); break;
                    case "My Backlog":      BacklogMenu(); break;
                    case "Ratings":         RatingsMenu(); break;
                    case "Recommendations": RecommendationsMenu(); break;
                    case "Account":         AccountMenu(); break;
                    case "Logout":          return;
                }
            }
        }

        private void AdminMenu()
        {
            Console.WriteLine("coming soon");
            Console.ReadLine();
            currentUser = null;
        }
        
        // menu for user's game library
        private void LibraryMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[green]{currentUser.username}'s Library[/]");
                Console.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Games", "Add Game", "Remove Game", "Back"));

                switch (choice)
                {
                    case "View Games":  ViewLibrary(); break;
                    case "Add Game":    AddGameToLibrary(); break;
                    case "Remove Game": RemoveGameFromLibrary(); break;
                    case "Back":        return;
                }
            }
        }
        // displays games in user's library
        private void ViewLibrary()
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
        private void AddGameToLibrary()
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
        private void RemoveGameFromLibrary()
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
        private void BacklogMenu()
        {
            Console.WriteLine("coming soon");
            Console.ReadLine();
        }
        private void RatingsMenu()
        {
            Console.WriteLine("coming soon");
            Console.ReadLine();
        }
        private void RecommendationsMenu()
        {
            Console.WriteLine("Coming soon");
            Console.ReadLine();
        }
        private void AccountMenu()
        {
            Console.WriteLine("Coming soon");
            Console.ReadLine();
        }
    }
}     