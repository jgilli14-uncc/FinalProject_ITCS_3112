using FinalProject3112.Models;
namespace FinalProject3112.Interfaces;

public interface IAuthSystem
{
    public bool Login(int userID, string password);
    public void Logout();
    public AuthLevel Verify(int userID);
}