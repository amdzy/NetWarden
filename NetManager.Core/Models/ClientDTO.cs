namespace NetManager.Core.Models;

public class SerializedClient
{
    public string Ip { get; set; } = string.Empty;
    public string Mac { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
    public bool HasNickname { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

