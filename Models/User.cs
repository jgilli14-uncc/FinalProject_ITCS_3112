namespace FinalProject3112.Models;
using System.Text.Json.Serialization;

public class User
{
    [JsonIgnore]
    public UserCredentials userCredentials { get; set; }
    public int userID { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    
    public User()
    {
        
    }
    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
        this.userCredentials = new UserCredentials(userID, username, password);
    }
    public void SetPassword(string newPassword)
    {
        password = newPassword;
    }
    public bool CheckPassword(string passwordToCheck)
    {
        return password == passwordToCheck;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Username: {username}");
    }
}