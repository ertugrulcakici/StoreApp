using System.Text.Json;

namespace StoreApp.Infrastructure.Extensions
{
    public static class SessionExtension
    {
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize<T>(value));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData is null 
                ? default(T) 
                : JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}
