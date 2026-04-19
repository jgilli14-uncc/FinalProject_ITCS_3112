public interface IAuthSystem
{
    public Login(int userID, string password);
    public void Logout();
    public AuthLevel Verify(int userID);
}