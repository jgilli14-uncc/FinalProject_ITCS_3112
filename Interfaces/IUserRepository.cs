public interface IUserRepository
{
    public void AddUser(User user);
    public void RemoveUser(User user);
    public User getUserbyID(int userID);
    public User ModifyUserName(int userID, string newName);
    public User ModifyUserPassword(int userID, string newPassword);
}