using System.Collections;

namespace CustomHashTable;

public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue?>> where TKey : notnull
{
    private int _capacity;
    private int _count;
    
    // taken is optimal by trial and experiment.
    // Also turns out to be used Java HashMap implementation.
    private const double LoadFactor = 0.75; 
    private DoublyLinkedList<KeyValuePair<TKey, TValue?>>[] _buckets;

    public HashTable(int capacity = 0)
    {
        _capacity = HashHelpers.GetPrime(capacity);
        _count = 0;
        _buckets = new DoublyLinkedList<KeyValuePair<TKey, TValue?>>[_capacity];

        for (var i = 0; i < _capacity; i += 1)
        {
            _buckets[i] = new DoublyLinkedList<KeyValuePair<TKey, TValue?>>();
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (_count / (double) _capacity > LoadFactor)
        {
            Resize();
        }

        var bucket = GetBucket(key);
        var item = new KeyValuePair<TKey, TValue?>(key, value);

        bucket.Add(item);
        _count++;
    }


    public void Remove(TKey key)
    {
        var bucket = GetBucket(key);
        bucket.Remove(new KeyValuePair<TKey, TValue?>(key, default));
        _count--;
    }

    public TValue? Get(TKey key)
    {
        var bucket = GetBucket(key);
        var node = bucket.Find(new KeyValuePair<TKey, TValue?>(key, default));
        if (node != null)
            return node.Data.Value;
        
        return default;
    }
    
    public void Set(TKey key, TValue value)
    {
        var bucket = GetBucket(key);
        var node = bucket.Find(new KeyValuePair<TKey, TValue?>(key, default));
        var data = new KeyValuePair<TKey, TValue?>(key, value);
        if (node != null)
            node.Data = data;
        else
        {
            bucket.Add(data);
        }
    }

    public TValue? this[TKey key]
    {
        get => Get(key);
        set => Set(key, value!);
    }

    private DoublyLinkedList<KeyValuePair<TKey, TValue?>> GetBucket(TKey key)
    {
        var hash = Hash(key);

        return _buckets[hash];
    }

    private uint Hash(TKey key)
    {
        return (uint) key.GetHashCode() % (uint) _capacity;
    }

    private void Resize()
    {
        var oldCapacity = _capacity;
        _capacity = HashHelpers.ExpandPrime(oldCapacity);
        var buckets = new DoublyLinkedList<KeyValuePair<TKey, TValue?>>[_capacity];
        for (var i = 0; i < _capacity; i += 1)
        {
            buckets[i] = new DoublyLinkedList<KeyValuePair<TKey, TValue?>>();
        }

        for (var i = 0; i < oldCapacity; i += 1)
        {
            foreach (var item in _buckets[i])
            {
                var hash = Hash(item.Key);
                buckets[hash].Add(new KeyValuePair<TKey, TValue?>(item.Key, item.Value));
            }
        }

        _buckets = buckets;
    }

    public IEnumerator<KeyValuePair<TKey, TValue?>> GetEnumerator()
    {
        foreach (var bucket in _buckets)
        {
            foreach (var item in bucket)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}