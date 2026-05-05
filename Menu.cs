using Spectre.Console;
using FinalProject3112.Models;
using FinalProject3112.Repositories;
using FinalProject3112.Services;

namespace FinalProject3112.Menus
{
    public class Menu
    {
        private BasicUser currentUser = null;
        private SuperUser currentAdmin = null;
        private GameRepository gameRepo = GameRepository.getInstance();

//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------LOGIN------------------------------------------------------
//----------------------------------------------------------------------------------------------------------

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
            AnsiConsole.MarkupLine("[green] Login [/]");
            Console.WriteLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = AuthSystem.getInstance().Login(username, password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid username or password.[/]");
                Console.ReadLine();
                return;
            }

            if (user is SuperUser)
                currentAdmin = user as SuperUser;
            else
                currentUser = user as BasicUser;
        }

        // new user registration
        private void Register()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green] Register [/]");
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

            BasicUser newUser = new BasicUser(username, password);
            UserRepository.getInstance().AddUser(newUser);
            currentUser = newUser;
        }

//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------MAIN MENU--------------------------------------------------
//----------------------------------------------------------------------------------------------------------
      
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
    
//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------LIBRARY----------------------------------------------------
//----------------------------------------------------------------------------------------------------------    
        
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

//----------------------------------------------------------------------------------------------------------
//-----------------------------------------------BACKLOG----------------------------------------------------
//----------------------------------------------------------------------------------------------------------

        private void BacklogMenu()
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
                case "View Backlog": ViewBacklog(); break;
                case "Update Status": UpdateStatus(); break;
                case "Back": return;
            }
        }

        private void ViewBacklog()
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

        private void UpdateStatus()
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
                    case "View My Ratings":  ViewRatings(); break;
                    case "Rate a Game":      RateGame(); break;
                    case "Edit a Rating":    EditRating(); break;
                    case "Remove a Rating":  RemoveRating(); break;
                    case "Back":             return;
                }
            }
        }

        private void ViewRatings()
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

        private void RateGame()
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

        private void EditRating()
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

        private void RemoveRating()
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
                    case "Get Recommendations": GetRecommendations(); break;
                    case "Back":                return;
                }
            }
        }

        private void GetRecommendations()
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
            List<Game> recommendations = strategy.GenerateRecommendations(currentUser.userID);

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
                
                currentUser.DisplayInfo();
                
                Console.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("Change Username", "Change Password", "Delete Account", "Back"));

                switch (choice)
                {
                    case "Change Username": ChangeUsername(); break;
                    case "Change Password": ChangePassword(); break;
                    case "Delete Account":  if (DeleteAccount()) return; break;
                    case "Back":            return;
                }
            }
        }
        private void ChangeUsername()
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
        private void ChangePassword()
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
        private bool DeleteAccount()
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
}     