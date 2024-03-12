using System.Text.Json.Serialization;

namespace NetWarden.Core.Models;

public class Vendor
{
    [JsonPropertyName("macPrefix")]
    public string? MacPrefix { get; set; }

    [JsonPropertyName("vendorName")]
    public string? VendorName { get; set; }
}