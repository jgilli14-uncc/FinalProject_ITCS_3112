using FinalProject3112.Interfaces;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

namespace FinalProject3112.Services;

public class AuthSystem : IAuthSystem
{
    private static AuthSystem instance;
    private int sessionID;

    private AuthSystem()
    {
        sessionID = 0;
    }

    public static AuthSystem getInstance()
    {
        if (instance == null)
        {
            instance = new AuthSystem();
        }
        return instance;
    }

    public User Login(string username, string password)
    {
        for (int i = 0; i < UserRepository.getInstance().userDatabase.Count; i++)
        {
            User user = UserRepository.getInstance().userDatabase[i];
            if (user.username == username && user.CheckPassword(password))
            {
                sessionID = user.userID;
                return user;
            }
        }
        return null;
    }

    public void Logout()
    {
        sessionID = 0;
    }

    public AuthLevel Verify(int userID)
    {
        User user = UserRepository.getInstance().getUserbyID(userID);
        if (user == null) return AuthLevel.NOT_VERIFIED;
        if (user is SuperUser) return AuthLevel.VERIFIED_ADMIN;
        return AuthLevel.VERIFIED;
    }
}