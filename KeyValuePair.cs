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

}