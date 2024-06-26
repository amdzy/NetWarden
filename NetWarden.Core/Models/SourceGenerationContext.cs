using System.Text.Json.Serialization;

namespace NetWarden.Core.Models;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Vendor))]
[JsonSerializable(typeof(List<Vendor>))]
[JsonSerializable(typeof(SerializedClient))]
[JsonSerializable(typeof(List<SerializedClient>))]
[JsonSerializable(typeof(Device))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}