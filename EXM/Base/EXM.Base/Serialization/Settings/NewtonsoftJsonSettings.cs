using EXM.Base.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace EXM.Base.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}