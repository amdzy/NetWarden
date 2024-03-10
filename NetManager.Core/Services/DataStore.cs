using System.Text.Json;
using NetManager.Core.Models;

namespace NetManager.Core.Services;

public static class DataStore
{
    public static string DataPath { get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "net-manager"); }

    public static IList<SerializedClient> LoadClients()
    {
        CheckDataDirCreated();
        var path = Path.Combine(DataPath, "data", "clients.json");

        using var fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read);
        if (fileStream.Length == 0)
        {
            return [];
        }
        return JsonSerializer.Deserialize(fileStream, typeof(List<SerializedClient>), SourceGenerationContext.Default) as List<SerializedClient> ?? [];
    }

    public static void SaveClients(IList<SerializedClient> clients)
    {
        CheckDataDirCreated();
        var path = Path.Combine(DataPath, "data", "clients.json");

        using var fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

        JsonSerializer.Serialize(fileStream, clients, typeof(List<SerializedClient>), SourceGenerationContext.Default);
    }

    private static void CheckDataDirCreated()
    {
        var path = Path.Combine(DataPath, "data");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
