using EXM.Common.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace EXM.Common.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}