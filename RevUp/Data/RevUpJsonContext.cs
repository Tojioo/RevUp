using System.Text.Json.Serialization;

namespace RevUp.Data;

/// <summary>Source-generated JSON for list columns, so serialization also works where reflection-based JSON is disabled (trimmed or AOT builds).</summary>
[JsonSerializable(typeof(List<string>))]
internal sealed partial class RevUpJsonContext : JsonSerializerContext
{
}