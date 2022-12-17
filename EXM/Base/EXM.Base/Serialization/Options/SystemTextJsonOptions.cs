using EXM.Base.Interfaces.Serialization.Options;
using System.Text.Json;

namespace EXM.Base.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}