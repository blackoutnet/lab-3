using System.Collections;

namespace CustomHashTable;

public class DoublyLinkedList<TData> : IEnumerable<TData>
{
    private DoublyLinkedListNode<TData>? _head;
    private int _count;

    public int Count => _count;

    public DoublyLinkedList()
    {
        _head = null;
    }

    public void Add(TData data)
    {
        var newNode = new DoublyLinkedListNode<TData>(data);
        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            _head.Previous = newNode;
            newNode.Next = _head;
            _head = newNode;
        }

        _count++;
    }

    public void Remove(TData data)
    {
        var node = Find(data);
        if (node != null)
        {
            if (node.Next != null)
            {
                node.Data = node.Next.Data;
                node.Next = node.Next.Next;
            }
            else if (node.Previous != null)
            {
                node.Data = node.Previous.Data;
                node.Previous = node.Previous.Next;
            }
            else
            {
                _head = null;
            }
        }

        _count++;
    }

    public DoublyLinkedListNode<TData>? Find(TData data)
    {
        var node = _head;
        if (_head == null)
        {
            return null;
        }

        while (node != null)
        {
            if (data.Equals(node.Data))
            {
                return node;
            }

            node = node.Next;
        }

        return null;
    }

    public bool Contains(TData data)
    {
        return Find(data) != null;
    }

    public IEnumerator<TData> GetEnumerator()
    {
        var node = _head;
        if (_head == null)
        {
            yield break;
        }

        while (node != null && node.Data != null)
        {
            yield return node.Data;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}