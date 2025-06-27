namespace Scratch.DataStructure;

public class ListNode
{
    public int val;
    public ListNode? next;

    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}

public class ListUtil
{
    /* 在链表的节点 n0 之后插入节点 P */
    public static void Insert(ListNode n0, ListNode p)
    {
        ListNode? n1 = n0.next;
        p.next = n1;
        n0.next = p;
    }

    /* 删除链表的节点 n0 之后的首个节点 */
    public static void Remove(ListNode n0)
    {
        if (n0.next == null)
            return;
        // n0 -> P -> n1
        ListNode p = n0.next;
        ListNode? n1 = p.next;
        n0.next = n1;
    }

    /* 访问链表中索引为 index 的节点 */
    public static ListNode? Access(ListNode? head, int index)
    {
        for (var i = 0; i < index; i++)
        {
            if (head == null)
                return null;
            head = head.next;
        }

        return head;
    }

    /* 在链表中查找值为 target 的首个节点 */
    public static int Find(ListNode? head, int target)
    {
        var index = 0;
        while (head != null)
        {
            if (head.val == target)
                return index;
            head = head.next;
            index++;
        }

        return -1;
    }
}

/* 列表类 */
public class ArrayList
{
    private int[] _arr; // 数组（存储列表元素）
    private int _arrCapacity = 10; // 列表容量
    private int _arrSize = 0; // 列表长度（当前元素数量）
    private readonly int _extendRatio = 2; // 每次列表扩容的倍数

    /* 构造方法 */
    public ArrayList()
    {
        _arr = new int[_arrCapacity];
    }

    /* 获取列表长度（当前元素数量）*/
    public int Size()
    {
        return _arrSize;
    }

    /* 获取列表容量 */
    public int Capacity()
    {
        return _arrCapacity;
    }

    /* 访问元素 */
    public int Get(int index)
    {
        // 索引如果越界，则抛出异常，下同
        if (index < 0 || index >= _arrSize)
            throw new IndexOutOfRangeException("索引越界");
        return _arr[index];
    }

    /* 更新元素 */
    public void Set(int index, int num)
    {
        if (index < 0 || index >= _arrSize)
            throw new IndexOutOfRangeException("索引越界");
        _arr[index] = num;
    }

    /* 在尾部添加元素 */
    public void Add(int num)
    {
        // 元素数量超出容量时，触发扩容机制
        if (_arrSize == _arrCapacity)
            ExtendCapacity();
        _arr[_arrSize] = num;
        // 更新元素数量
        _arrSize++;
    }

    /* 在中间插入元素 */
    public void Insert(int index, int num)
    {
        if (index < 0 || index >= _arrSize)
            throw new IndexOutOfRangeException("索引越界");
        // 元素数量超出容量时，触发扩容机制
        if (_arrSize == _arrCapacity)
            ExtendCapacity();
        // 将索引 index 以及之后的元素都向后移动一位
        for (var j = _arrSize - 1; j >= index; j--)
        {
            _arr[j + 1] = _arr[j];
        }

        _arr[index] = num;
        // 更新元素数量
        _arrSize++;
    }

    /* 删除元素 */
    public int Remove(int index)
    {
        if (index < 0 || index >= _arrSize)
            throw new IndexOutOfRangeException("索引越界");
        var num = _arr[index];
        // 将将索引 index 之后的元素都向前移动一位
        for (var j = index; j < _arrSize - 1; j++)
        {
            _arr[j] = _arr[j + 1];
        }

        // 更新元素数量
        _arrSize--;
        // 返回被删除的元素
        return num;
    }

    /* 列表扩容 */
    public void ExtendCapacity()
    {
        // 新建一个长度为 arrCapacity * extendRatio 的数组，并将原数组复制到新数组
        Array.Resize(ref _arr, _arrCapacity * _extendRatio);
        // 更新列表容量
        _arrCapacity = _arr.Length;
    }

    /* 将列表转换为数组 */
    public int[] ToArray()
    {
        // 仅转换有效长度范围内的列表元素
        var arr = new int[_arrSize];
        for (var i = 0; i < _arrSize; i++)
        {
            arr[i] = Get(i);
        }

        return arr;
    }
}
