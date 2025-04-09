using System.Text.Json;

namespace nps;

public static class SessionExtensions
{
    public static void Add<T>(this ISession session, string key, T value)
    {
        session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.Get(key);
        if (value is null)
        {
            throw new KeyNotFoundException($"The given {key} was not present in the session.");
        }
        return JsonSerializer.Deserialize<T>(value);
    }

    public static bool ContainsKey(this ISession session, string key)
    {
        return session.Get(key) != null;
    }

    public static bool TryGetValue<T>(this ISession session, string key, out T value)
    {
        var bytes = session.Get(key);

        if (bytes is null || bytes.Length == 0)
        {
            value = default!;
            return false;
        }
        
        value = JsonSerializer.Deserialize<T>(bytes)!;
        return true;
    }
}