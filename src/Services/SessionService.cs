    namespace FinalProject3112.Services;
    using Spectre.Console;
    using FinalProject3112.Interfaces;
    using FinalProject3112.Models;
    using FinalProject3112.Repositories;

    public class SessionService : ISessionService
    {
        private IUserRepository userRepository;
        public BasicUser CurrentUser { get; set; }
        public SuperUser CurrentAdmin { get; set; }

        public SessionService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // user login
        public void Login()
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
                CurrentAdmin = user as SuperUser;
            else
                CurrentUser = user as BasicUser;
        }

        // new user registration
        public void Register()
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
            CurrentUser = newUser;
        }
        public void Logout()
        {
            AuthSystem.getInstance().Logout();
            CurrentUser = null;
            CurrentAdmin = null;
        }
    }