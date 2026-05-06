namespace FinalProject3112.Services;
using System.Text.Json;
using FinalProject3112.Models;
using FinalProject3112.Repositories;

//change to fileService

//make an interface for this class

public abstract class FileService
{
    public void Load()
    {
        string path = GetFilePath();
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json)) return;
        Deserialize(json);
    }

    public void Save()
    {
        string path = GetFilePath();
        string json = Serialize();
        File.WriteAllText(path, json);
    }

    protected abstract string GetFilePath();
    protected abstract void Deserialize(string json);
    protected abstract string Serialize();
}