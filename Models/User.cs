public class User
{
    public int userID { get; set; }
    public string username { get; set; }
    private string password { get; set; }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
    }
}