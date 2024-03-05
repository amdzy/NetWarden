using System.Text.Json.Serialization;

namespace NetManager.Core.Models;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Vendor))]
[JsonSerializable(typeof(List<Vendor>))]
[JsonSerializable(typeof(Client))]
[JsonSerializable(typeof(List<Client>))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}