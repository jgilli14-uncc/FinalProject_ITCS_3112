using FinalProject3112.Interfaces;

public class UserRepository : IUserRepository
{
    private UserRepository instance;
    private List<User> userDatabase;

    private void UserRepository()
    {
        
    }

    public UserRepository getInstance()
    {
        return instance;
    }

    public AddUser(string name, string password, int vUserID)
    {
        
    }

    public RemoveUser(int userID, int vUserID)
    {
        
    }

    public ModifyUserName(int userId, string newName, int vUserID)
    {
        
    }

    public ModifyUserPassword(int userID, string newPassword, int vUserID)
    {
        
    }
}