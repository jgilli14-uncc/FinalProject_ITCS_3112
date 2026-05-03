namespace FinalProject3112.Models;

public struct UserCredentials
{
    public int userID;
    public string username;
    private string password;
    
    public UserCredentials(int userID, string username, string password)
    {
        this.userID = userID;
        this.username = username;
        this.password = password;
    }
}