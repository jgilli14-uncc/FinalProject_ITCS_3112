namespace FinalProject3112.Models;

public class SuperUser : User
{
    private bool adminCredential { get; set; }

    public SuperUser(string username, string password) : base(username, password)
    {
        adminCredential = true;
    }
    public new void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
        Console.WriteLine($"Admin Credentials: {adminCredential}");
    }
}