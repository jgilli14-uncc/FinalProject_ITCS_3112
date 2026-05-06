namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IBacklogService
{
    void ViewBacklog(BasicUser currentUser);
    void UpdateStatus(BasicUser currentUser);
}