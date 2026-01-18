namespace Scratch.Labuladong;

public class ListNode(int val = 0, ListNode? next = null)
{
    public int val = val;
    public ListNode? next = next;
}

public class MonotonicQueue<T> where T: IComparable<T>
{
    // 常规队列，存储所有元素
    LinkedList<T> q = new LinkedList<T>();

    // 元素降序排列的单调队列，头部是最大值
    LinkedList<T> maxQ = new LinkedList<T>();

    // 元素升序排列的单调队列，头部是最小值
    LinkedList<T> minQ = new LinkedList<T>();

    public void push(T elem)
    {
        // 维护常规队列，直接在队尾插入元素
        q.AddLast(elem);

        // 维护 maxQ，将小于 elem 的元素全部删除
        while (maxQ.Count != 0 && maxQ.Last!.Value.CompareTo(elem) < 0)
        {
            maxQ.RemoveLast();
        }

        maxQ.AddLast(elem);

        // 维护 minQ，将大于 elem 的元素全部删除
        while (minQ.Count != 0 && minQ.Last!.Value.CompareTo(elem) > 0)
        {
            minQ.RemoveLast();
        }

        minQ.AddLast(elem);
    }

    public T Max()
    {
        // maxQ 的头部是最大元素
        return maxQ.First!.Value;
    }

    public T Min()
    {
        // minQ 的头部是最小元素
        return minQ.First!.Value;
    }

    public T Pop()
    {
        // 从标准队列头部弹出需要删除的元素
        var deleteVal = q.First!.Value;
        q.RemoveFirst();

        // 由于 push 的时候会删除元素，deleteVal 可能已经被删掉了
        if (deleteVal.Equals(maxQ.First!.Value))
        {
            maxQ.RemoveFirst();
        }

        if (deleteVal.Equals(minQ.First!.Value))
        {
            minQ.RemoveFirst();
        }

        return deleteVal;
    }

    public int Size()
    {
        // 标准队列的大小即是当前队列的大小
        return q.Count;
    }

    public bool IsEmpty()
    {
        return q.Count == 0;
    }
}

public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
