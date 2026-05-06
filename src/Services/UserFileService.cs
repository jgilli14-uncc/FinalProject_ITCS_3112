namespace FinalProject3112.Services;
using System.Text.Json;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

public class UserFileService : FileService
{
    protected override string GetFilePath()
    {
        return "src/data/users.json";
    }

    protected override void Deserialize(string json)
    {
        List<BasicUser> users = JsonSerializer.Deserialize<List<BasicUser>>(json);
        if (users == null) return;
        UserRepository.getInstance().userDatabase.Clear();
        for (int i = 0; i < users.Count; i++)
            UserRepository.getInstance().userDatabase.Add(users[i]);
        UserRepository.getInstance().userIdCounter = users.Count + 1;
    }

    protected override string Serialize()
    {
        List<BasicUser> users = new List<BasicUser>();
        for (int i = 0; i < UserRepository.getInstance().userDatabase.Count; i++)
        {
            BasicUser u = UserRepository.getInstance().userDatabase[i] as BasicUser;
            if (u != null)
                users.Add(u);
        }
        return JsonSerializer.Serialize(users);
    }
}