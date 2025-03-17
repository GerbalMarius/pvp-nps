using System.Text.Json;

namespace nps;

public static class SessionExtensions
{
    public static void Add<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null)
        {
            throw new KeyNotFoundException($"The given {key} was not present in the session.");
        }
        return JsonSerializer.Deserialize<T>(value);
    }

    public static bool ContainsKey<T>(this ISession session, string key)
    {
        try
        {
            _ = session.Get<T>(key);
            return true;
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
    }
}