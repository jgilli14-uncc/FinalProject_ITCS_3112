using FinalProject3112.Models;
namespace FinalProject3112.Interfaces;

public interface IAuthSystem
{
    public User Login(string username, string password);
    public void Logout();
    public AuthLevel Verify(int userID);
}