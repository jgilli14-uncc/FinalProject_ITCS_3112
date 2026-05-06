namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface ISessionService
{
    BasicUser CurrentUser { get; set; }
    SuperUser CurrentAdmin { get; set; }
    void Login();
    void Register();
    void Logout();
}