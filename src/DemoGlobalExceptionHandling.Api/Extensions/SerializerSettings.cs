using Newtonsoft.Json;

namespace DemoGlobalExceptionHandling.Api.Extensions
{
    public static class SerializerSettings
    {
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}