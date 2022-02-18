namespace CustomHashTable;

public class DoublyLinkedListNode<TData>
{
    public TData Data { get; set; }

    public DoublyLinkedListNode<TData>? Next { get; set; }
    public DoublyLinkedListNode<TData>? Previous { get; set; }

    public DoublyLinkedListNode(TData data,
        DoublyLinkedListNode<TData>? next = null,
        DoublyLinkedListNode<TData>? previous = null)
    {
        Data = data;
        Next = next;
        Previous = previous;
    }
}