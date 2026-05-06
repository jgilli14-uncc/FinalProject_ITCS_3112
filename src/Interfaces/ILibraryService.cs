namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface ILibraryService
{
    void ViewLibrary(BasicUser currentUser);
    void AddGameToLibrary(BasicUser currentUser);
    void RemoveGameFromLibrary(BasicUser currentUser);
}