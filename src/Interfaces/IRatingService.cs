namespace FinalProject3112.Interfaces;
using FinalProject3112.Models;

public interface IRatingService
{
    void ViewRatings(BasicUser currentUser);
    void RateGame(BasicUser currentUser);
    void EditRating(BasicUser currentUser);
    void RemoveRating(BasicUser currentUser);
}