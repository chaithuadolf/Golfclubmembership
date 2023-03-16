using Newtonsoft.Json;

namespace Golfclubmembership
{
    public static class SessionObjectHandler
    {
        public static void SetObjectAsJsonString(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJsonString<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
