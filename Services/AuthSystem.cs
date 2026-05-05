using FinalProject3112.Interfaces;
using FinalProject3112.Models;

public class AuthSystem : IAuthSystem
{
    private static AuthSystem instance;
    private int sessionID;

    private AuthSystem()
    {
        sessionID = 0;
    }

    private static AuthSystem getInstance()
    {
        if (instance == null)
        {
            instance = new AuthSystem();
        }
        return instance;
    }

    public bool Login(int userID, string password)
    {
        if (userID < 0)
        {
            Console.WriteLine("Invalid User ID");
        }

        if (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Invalid Password");
        }
        return true;
    }

    public void Logout()
    {
        
    }

    public AuthLevel Verify(int userID)
    {
        
    }
}