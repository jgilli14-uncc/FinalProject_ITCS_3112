namespace FinalProject3112.Repositories;
using FinalProject3112.Models;
using FinalProject3112.Interfaces;

public class UserRepository : IUserRepository
{
    public static UserRepository instance;
    public List<User> userDatabase { get; set; }

    private UserRepository()
    {
        userDatabase = new List<User>();
    }
    public static UserRepository getInstance()
    {
        if (instance == null)
            instance = new UserRepository();
        return instance;
    }
    public void AddUser(User user)
    {
        userDatabase.Add(user);
    }
    public void RemoveUser(User user)
    {
        userDatabase.Remove(user);
    }

    public User getUserbyID(int userID)
    {
        foreach (User user in userDatabase)
        {
            if (user.userID == userID)
                return user;
        }
        return null;
    }
    public User ModifyUserName(int userID, string newName)
    {
        User user = getUserbyID(userID);
        if (user != null)
            user.username = newName;
        return user;
    }

    public User ModifyUserPassword(int userID, string newPassword)
    {
        User user = getUserbyID(userID);
        if (user != null)
            user.SetPassword(newPassword);
        return user;
    }
}