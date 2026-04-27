public class SuperUser
{
    public int userID { get; set; }
    public string username { get; set; }
    private string password { get; set; }
    private bool adminCredential { get; set; }

    public SuperUser(string username, string password)
    {
        this.username = username;
        this.password = password;
        adminCredential = true;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
        Console.WriteLine($"Admin Credentials: {adminCredential}");
    }
}