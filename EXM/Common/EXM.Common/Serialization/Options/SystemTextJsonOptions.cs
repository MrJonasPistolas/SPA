using EXM.Common.Interfaces.Serialization.Options;
using System.Text.Json;

namespace EXM.Common.Serialization.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}