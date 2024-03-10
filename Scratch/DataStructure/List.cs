namespace Algorithm.DataStructure;

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
    public static void Insert(ListNode n0, ListNode P)
    {
        ListNode? n1 = n0.next;
        P.next = n1;
        n0.next = P;
    }

    /* 删除链表的节点 n0 之后的首个节点 */
    public static void Remove(ListNode n0)
    {
        if (n0.next == null)
            return;
        // n0 -> P -> n1
        ListNode P = n0.next;
        ListNode? n1 = P.next;
        n0.next = n1;
    }

    /* 访问链表中索引为 index 的节点 */
    public static ListNode? Access(ListNode? head, int index)
    {
        for (int i = 0; i < index; i++)
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
        int index = 0;
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
    private int[] arr;           // 数组（存储列表元素）
    private int arrCapacity = 10;    // 列表容量
    private int arrSize = 0;         // 列表长度（当前元素数量）
    private readonly int extendRatio = 2;  // 每次列表扩容的倍数

    /* 构造方法 */
    public ArrayList()
    {
        arr = new int[arrCapacity];
    }

    /* 获取列表长度（当前元素数量）*/
    public int Size()
    {
        return arrSize;
    }

    /* 获取列表容量 */
    public int Capacity()
    {
        return arrCapacity;
    }

    /* 访问元素 */
    public int Get(int index)
    {
        // 索引如果越界，则抛出异常，下同
        if (index < 0 || index >= arrSize)
            throw new IndexOutOfRangeException("索引越界");
        return arr[index];
    }

    /* 更新元素 */
    public void Set(int index, int num)
    {
        if (index < 0 || index >= arrSize)
            throw new IndexOutOfRangeException("索引越界");
        arr[index] = num;
    }

    /* 在尾部添加元素 */
    public void Add(int num)
    {
        // 元素数量超出容量时，触发扩容机制
        if (arrSize == arrCapacity)
            ExtendCapacity();
        arr[arrSize] = num;
        // 更新元素数量
        arrSize++;
    }

    /* 在中间插入元素 */
    public void Insert(int index, int num)
    {
        if (index < 0 || index >= arrSize)
            throw new IndexOutOfRangeException("索引越界");
        // 元素数量超出容量时，触发扩容机制
        if (arrSize == arrCapacity)
            ExtendCapacity();
        // 将索引 index 以及之后的元素都向后移动一位
        for (int j = arrSize - 1; j >= index; j--)
        {
            arr[j + 1] = arr[j];
        }
        arr[index] = num;
        // 更新元素数量
        arrSize++;
    }

    /* 删除元素 */
    public int Remove(int index)
    {
        if (index < 0 || index >= arrSize)
            throw new IndexOutOfRangeException("索引越界");
        int num = arr[index];
        // 将将索引 index 之后的元素都向前移动一位
        for (int j = index; j < arrSize - 1; j++)
        {
            arr[j] = arr[j + 1];
        }
        // 更新元素数量
        arrSize--;
        // 返回被删除的元素
        return num;
    }

    /* 列表扩容 */
    public void ExtendCapacity()
    {
        // 新建一个长度为 arrCapacity * extendRatio 的数组，并将原数组复制到新数组
        Array.Resize(ref arr, arrCapacity * extendRatio);
        // 更新列表容量
        arrCapacity = arr.Length;
    }

    /* 将列表转换为数组 */
    public int[] ToArray()
    {
        // 仅转换有效长度范围内的列表元素
        int[] arr = new int[arrSize];
        for (int i = 0; i < arrSize; i++)
        {
            arr[i] = Get(i);
        }
        return arr;
    }
}
