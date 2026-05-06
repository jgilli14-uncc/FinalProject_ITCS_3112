namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IAccountService
{
    void ChangeUsername(BasicUser currentUser);
    void ChangePassword(BasicUser currentUser);
    bool DeleteAccount(BasicUser currentUser);
}