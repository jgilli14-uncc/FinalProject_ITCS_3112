using Spectre.Console;
using FinalProject3112.Models;
using FinalProject3112.Repositories;
using FinalProject3112.Services;
using FinalProject3112.Interfaces;
 
namespace FinalProject3112.Services
{
    public class Menu
    {
        private ILibraryService libraryService = new LibraryService(GameRepository.getInstance());
        private IBacklogService backlogService = new BacklogService();
        private IRatingService ratingService = new RatingService(RatingRepository.getInstance());
        private IRecommendationService recommendationService = new RecommendationService(GameRepository.getInstance());
        private IAccountService accountService = new AccountService(UserRepository.getInstance());
        private ISessionService sessionService = new SessionService(UserRepository.getInstance());
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------LOGIN------------------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        public void Start()
        {
            while (true)
            {
                if (sessionService.CurrentUser == null && sessionService.CurrentAdmin == null)
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Welcome to the Game Recommendation System! Please Login or Register:")
                            .AddChoices("Login", "Register", "Exit"));
 
                    switch (choice)
                    {
                        case "Login":    sessionService.Login(); break;
                        case "Register": sessionService.Register(); break;
                        case "Exit":     return;
                    }
                }
                else
                {
                    if (sessionService.CurrentAdmin is SuperUser)
                        AdminMenu();
                    else
                        MainMenu();
 
                    sessionService.Logout();
                }
            }
        }
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------MAIN MENU--------------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        private void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[green]Welcome, {sessionService.CurrentUser.username}![/]");
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
            sessionService.Logout();
        }
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------LIBRARY----------------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        // menu for user's game library
        private void LibraryMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[green]{sessionService.CurrentUser.username}'s Library[/]");
                Console.WriteLine();
 
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Games", "Add Game", "Remove Game", "Back"));
 
                switch (choice)
                {
                    case "View Games":  libraryService.ViewLibrary(sessionService.CurrentUser); break;
                    case "Add Game":    libraryService.AddGameToLibrary(sessionService.CurrentUser); break;
                    case "Remove Game": libraryService.RemoveGameFromLibrary(sessionService.CurrentUser); break;
                    case "Back":        return;
                }
            }
        }
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------BACKLOG----------------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        private void BacklogMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]My Backlog[/]");
                Console.WriteLine();
 
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View Backlog", "Update Status", "Back"));
 
                switch (choice)
                {
                    case "View Backlog":  backlogService.ViewBacklog(sessionService.CurrentUser); break;
                    case "Update Status": backlogService.UpdateStatus(sessionService.CurrentUser); break;
                    case "Back":          return;
                }
            }
        }
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------RATINGS----------------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        private void RatingsMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Ratings[/]");
                Console.WriteLine();
 
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View My Ratings", "Rate a Game", "Edit a Rating", "Remove a Rating", "Back"));
 
                switch (choice)
                {
                    case "View My Ratings":  ratingService.ViewRatings(sessionService.CurrentUser); break;
                    case "Rate a Game":      ratingService.RateGame(sessionService.CurrentUser); break;
                    case "Edit a Rating":    ratingService.EditRating(sessionService.CurrentUser); break;
                    case "Remove a Rating":  ratingService.RemoveRating(sessionService.CurrentUser); break;
                    case "Back":             return;
                }
            }
        }
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------RECOMMENDATIONS--------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        private void RecommendationsMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Recommendations[/]");
                Console.WriteLine();
 
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("Get Recommendations", "Back"));
 
                switch (choice)
                {
                    case "Get Recommendations": recommendationService.GetRecommendations(sessionService.CurrentUser); break;
                    case "Back":                return;
                }
            }
        }
 
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------ACCOUNT----------------------------------------------------
//----------------------------------------------------------------------------------------------------------
 
        private void AccountMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green] Account [/]");
                Console.WriteLine();
 
                sessionService.CurrentUser.DisplayInfo();
 
                Console.WriteLine();
 
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("Change Username", "Change Password", "Delete Account", "Back"));
 
                switch (choice)
                {
                    case "Change Username": accountService.ChangeUsername(sessionService.CurrentUser); break;
                    case "Change Password": accountService.ChangePassword(sessionService.CurrentUser); break;
                    case "Delete Account":  if (accountService.DeleteAccount(sessionService.CurrentUser)) return; break;
                    case "Back":            return;
                }
            }
        }
    }
}