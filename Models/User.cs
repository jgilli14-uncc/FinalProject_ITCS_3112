namespace FinalProject3112.Models;

public class User
{
    public int userID { get; set; }
    public string username { get; set; }
    protected string password { get; set; }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public void SetPassword(string newPassword)
    {
        password = newPassword;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
    }
}