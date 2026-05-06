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
            FileLoad.getInstance().LoadGamesFile();
            FileLoad.getInstance().LoadUsersFile();
            FileLoad.getInstance().LoadRatingsFile();

            Menu menu = new Menu();
            menu.Start();

            FileLoad.getInstance().SaveGamesFile();
            FileLoad.getInstance().SaveUsersFile();
            FileLoad.getInstance().SaveRatingsFile();
        }
    }
}