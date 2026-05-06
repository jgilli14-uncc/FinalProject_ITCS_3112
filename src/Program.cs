using FinalProject3112.Services;
using FinalProject3112.Repositories;
using FinalProject3112.Interfaces;
using FinalProject3112.Models;


namespace FinalProject3112
{
    class Program
    {
        static void Main(string[] args)
        {
            new GamesFileService().Load();
            new UserFileService().Load();
            new RatingFileService().Load();

            Menu menu = new Menu();
            menu.Start();

            new GamesFileService().Save();
            new UserFileService().Save();
            new RatingFileService().Save();
        }
    }
}