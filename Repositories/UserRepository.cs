public class UserRepository : IUserRepository
{
    private static UserRepository instance;
    public List<User> userDatabase { get; set; }

    private UserRepository()
    {
        userDatabase = new List<User>();
    }
    private static UserRepository getInstance()
    {
        if (instance == null)
        {
            instance = new UserRepository();
        }
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
    
}