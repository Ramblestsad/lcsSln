namespace Algorithm.DataStructure;

/* 基于链表实现的栈 */
public class LinkedListStack
{
    ListNode? stackPeek;  // 将头节点作为栈顶
    int stkSize = 0;   // 栈的长度

    public LinkedListStack()
    {
        stackPeek = null;
    }

    /* 获取栈的长度 */
    public int Size()
    {
        return stkSize;
    }

    /* 判断栈是否为空 */
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入栈 */
    public void Push(int num)
    {
        ListNode node = new(num)
        {
            next = stackPeek
        };
        stackPeek = node;
        stkSize++;
    }

    /* 出栈 */
    public int Pop()
    {
        int num = Peek();
        stackPeek = stackPeek!.next;
        stkSize--;
        return num;
    }

    /* 访问栈顶元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return stackPeek!.val;
    }

    /* 将 List 转化为 Array 并返回 */
    public int[] ToArray()
    {
        if (stackPeek == null)
            return [];

        ListNode? node = stackPeek;
        int[] res = new int[Size()];
        for (int i = res.Length - 1; i >= 0; i--)
        {
            res[i] = node!.val;
            node = node.next;
        }
        return res;
    }
}


/* 基于数组实现的栈 */
public class ArrayStack
{
    List<int> stack;
    public ArrayStack()
    {
        // 初始化列表（动态数组）
        stack = [];
    }

    /* 获取栈的长度 */
    public int Size()
    {
        return stack.Count;
    }

    /* 判断栈是否为空 */
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入栈 */
    public void Push(int num)
    {
        stack.Add(num);
    }

    /* 出栈 */
    public int Pop()
    {
        if (IsEmpty())
            throw new Exception();
        var val = Peek();
        stack.RemoveAt(Size() - 1);
        return val;
    }

    /* 访问栈顶元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return stack[Size() - 1];
    }

    /* 将 List 转化为 Array 并返回 */
    public int[] ToArray()
    {
        return [.. stack];
    }
}

/* 基于链表实现的队列 */
public class LinkedListQueue
{
    ListNode? front, rear;  // 头节点 front ，尾节点 rear
    int queSize = 0;

    public LinkedListQueue()
    {
        front = null;
        rear = null;
    }

    /* 获取队列的长度 */
    public int Size()
    {
        return queSize;
    }

    /* 判断队列是否为空 */
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入队 */
    public void Push(int num)
    {
        // 在尾节点后添加 num
        ListNode node = new(num);
        // 如果队列为空，则令头、尾节点都指向该节点
        if (front == null)
        {
            front = node;
            rear = node;
            // 如果队列不为空，则将该节点添加到尾节点后
        }
        else if (rear != null)
        {
            rear.next = node;
            rear = node;
        }
        queSize++;
    }

    /* 出队 */
    public int Pop()
    {
        int num = Peek();
        // 删除头节点
        front = front?.next;
        queSize--;
        return num;
    }

    /* 访问队首元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return front!.val;
    }

    /* 将链表转化为 Array 并返回 */
    public int[] ToArray()
    {
        if (front == null)
            return [];

        ListNode? node = front;
        int[] res = new int[Size()];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = node!.val;
            node = node.next;
        }
        return res;
    }
}

/* 基于环形数组实现的队列 */
public class ArrayQueue
{
    int[] nums;  // 用于存储队列元素的数组
    int front;   // 队首指针，指向队首元素
    int queSize; // 队列长度

    public ArrayQueue(int capacity)
    {
        nums = new int[capacity];
        front = queSize = 0;
    }

    /* 获取队列的容量 */
    int Capacity()
    {
        return nums.Length;
    }

    /* 获取队列的长度 */
    public int Size()
    {
        return queSize;
    }

    /* 判断队列是否为空 */
    public bool IsEmpty()
    {
        return queSize == 0;
    }

    /* 入队 */
    public void Push(int num)
    {
        if (queSize == Capacity())
        {
            Console.WriteLine("队列已满");
            return;
        }
        // 计算队尾指针，指向队尾索引 + 1
        // 通过取余操作实现 rear 越过数组尾部后回到头部
        int rear = ( front + queSize ) % Capacity();
        // 将 num 添加至队尾
        nums[rear] = num;
        queSize++;
    }

    /* 出队 */
    public int Pop()
    {
        int num = Peek();
        // 队首指针向后移动一位，若越过尾部，则返回到数组头部
        front = ( front + 1 ) % Capacity();
        queSize--;
        return num;
    }

    /* 访问队首元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return nums[front];
    }

    /* 返回数组 */
    public int[] ToArray()
    {
        // 仅转换有效长度范围内的列表元素
        int[] res = new int[queSize];
        for (int i = 0, j = front; i < queSize; i++, j++)
        {
            res[i] = nums[j % this.Capacity()];
        }
        return res;
    }
}

/* 双向链表节点 */
class BiListNode(int val)
{
    public int val = val;       // 节点值
    public BiListNode? next = null; // 后继节点引用
    public BiListNode? prev = null; // 前驱节点引用
}

/* 基于双向链表实现的双向队列 */
public class BiLinkedListDeque
{
    BiListNode? front, rear; // 头节点 front, 尾节点 rear
    int queSize = 0;      // 双向队列的长度

    public BiLinkedListDeque()
    {
        front = null;
        rear = null;
    }

    /* 获取双向队列的长度 */
    public int Size()
    {
        return queSize;
    }

    /* 判断双向队列是否为空 */
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入队操作 */
    void Push(int num, bool isFront)
    {
        BiListNode node = new(num);
        // 若链表为空，则令 front 和 rear 都指向 node
        if (IsEmpty())
        {
            front = node;
            rear = node;
        }
        // 队首入队操作
        else if (isFront)
        {
            // 将 node 添加至链表头部
            front!.prev = node;
            node.next = front;
            front = node; // 更新头节点
        }
        // 队尾入队操作
        else
        {
            // 将 node 添加至链表尾部
            rear!.next = node;
            node.prev = rear;
            rear = node;  // 更新尾节点
        }

        queSize++; // 更新队列长度
    }

    /* 队首入队 */
    public void PushFirst(int num)
    {
        Push(num, true);
    }

    /* 队尾入队 */
    public void PushLast(int num)
    {
        Push(num, false);
    }

    /* 出队操作 */
    int? Pop(bool isFront)
    {
        if (IsEmpty())
            throw new Exception();
        int? val;
        // 队首出队操作
        if (isFront)
        {
            val = front?.val; // 暂存头节点值
            // 删除头节点
            BiListNode? fNext = front?.next;
            if (fNext != null)
            {
                fNext.prev = null;
                front!.next = null;
            }
            front = fNext;   // 更新头节点
        }
        // 队尾出队操作
        else
        {
            val = rear?.val;  // 暂存尾节点值
            // 删除尾节点
            BiListNode? rPrev = rear?.prev;
            if (rPrev != null)
            {
                rPrev.next = null;
                rear!.prev = null;
            }
            rear = rPrev;    // 更新尾节点
        }

        queSize--; // 更新队列长度
        return val;
    }

    /* 队首出队 */
    public int? PopFirst()
    {
        return Pop(true);
    }

    /* 队尾出队 */
    public int? PopLast()
    {
        return Pop(false);
    }

    /* 访问队首元素 */
    public int? PeekFirst()
    {
        if (IsEmpty())
            throw new Exception();
        return front?.val;
    }

    /* 访问队尾元素 */
    public int? PeekLast()
    {
        if (IsEmpty())
            throw new Exception();
        return rear?.val;
    }

    /* 返回数组用于打印 */
    public int?[] ToArray()
    {
        BiListNode? node = front;
        int?[] res = new int?[Size()];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = node?.val;
            node = node?.next;
        }

        return res;
    }
}

/* 基于环形数组实现的双向队列 */
public class ArrayDeque
{
    int[] nums;  // 用于存储双向队列元素的数组
    int front;   // 队首指针，指向队首元素
    int queSize; // 双向队列长度

    /* 构造方法 */
    public ArrayDeque(int capacity)
    {
        nums = new int[capacity];
        front = queSize = 0;
    }

    /* 获取双向队列的容量 */
    int Capacity()
    {
        return nums.Length;
    }

    /* 获取双向队列的长度 */
    public int Size()
    {
        return queSize;
    }

    /* 判断双向队列是否为空 */
    public bool IsEmpty()
    {
        return queSize == 0;
    }

    /* 计算环形数组索引 */
    int Index(int i)
    {
        // 通过取余操作实现数组首尾相连
        // 当 i 越过数组尾部后，回到头部
        // 当 i 越过数组头部后，回到尾部
        return ( i + Capacity() ) % Capacity();
    }

    /* 队首入队 */
    public void PushFirst(int num)
    {
        if (queSize == Capacity())
        {
            Console.WriteLine("双向队列已满");
            return;
        }
        // 队首指针向左移动一位
        // 通过取余操作实现 front 越过数组头部后回到尾部
        front = Index(front - 1);
        // 将 num 添加至队首
        nums[front] = num;
        queSize++;
    }

    /* 队尾入队 */
    public void PushLast(int num)
    {
        if (queSize == Capacity())
        {
            Console.WriteLine("双向队列已满");
            return;
        }
        // 计算队尾指针，指向队尾索引 + 1
        int rear = Index(front + queSize);
        // 将 num 添加至队尾
        nums[rear] = num;
        queSize++;
    }

    /* 队首出队 */
    public int PopFirst()
    {
        int num = PeekFirst();
        // 队首指针向后移动一位
        front = Index(front + 1);
        queSize--;
        return num;
    }

    /* 队尾出队 */
    public int PopLast()
    {
        int num = PeekLast();
        queSize--;
        return num;
    }

    /* 访问队首元素 */
    public int PeekFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }
        return nums[front];
    }

    /* 访问队尾元素 */
    public int PeekLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }
        // 计算尾元素索引
        int last = Index(front + queSize - 1);
        return nums[last];
    }

    /* 返回数组用于打印 */
    public int[] ToArray()
    {
        // 仅转换有效长度范围内的列表元素
        int[] res = new int[queSize];
        for (int i = 0, j = front; i < queSize; i++, j++)
        {
            res[i] = nums[Index(j)];
        }
        return res;
    }
}
