namespace CustomHashTable;

public class KeyValuePair<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public KeyValuePair(TKey key, TValue value)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        Key = key;
        Value = value;
    }

    public bool Equals(KeyValuePair<TKey, TValue> other)
    {
        return Key.Equals(other.Key);
    }

    public override bool Equals(object? obj)
    {
        return obj is KeyValuePair<TKey, TValue> keyValue && Equals(keyValue);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    public static bool operator ==(KeyValuePair<TKey, TValue>? obj1, KeyValuePair<TKey, TValue>? obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;

        return obj1.Equals(obj2);
    }

    public static bool operator !=(KeyValuePair<TKey, TValue> obj1, KeyValuePair<TKey, TValue> obj2)
    {
        return !(obj1 == obj2);
    }
}