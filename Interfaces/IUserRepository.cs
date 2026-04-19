namespace FinalProject3112.Interfaces;
public interface IUserRepository
{
    public AddUser(string name, string password, int vUserID);
    public RemoveUser (int userID, int vUserID);
    public ModifyUserName (int userId,  string newName, int vUserID);
    public ModifyUserPassword(int userID, string newPassword, int vUserID);
}