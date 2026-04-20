using FinalProject3112.Enums;
namespace FinalProject3112.Interfaces;

public interface IAuthSystem
{
    public Login(int userID, string password);
    public void Logout();
    public AuthLevel Verify(int userID);
}