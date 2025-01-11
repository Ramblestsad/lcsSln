namespace Algorithm.DataStructure;

/* 基于链表实现的栈 */
public class LinkedListStack
{
    ListNode? _stackPeek = null; // 将头节点作为栈顶
    int _stkSize = 0; // 栈的长度

    /* 获取栈的长度 */
    public int Size()
    {
        return _stkSize;
    }

    /* 判断栈是否为空 */
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入栈 */
    public void Push(int num)
    {
        var node = new ListNode(num) { next = _stackPeek };
        _stackPeek = node;
        _stkSize++;
    }

    /* 出栈 */
    public int? Pop()
    {
        var num = Peek();
        _stackPeek = _stackPeek?.next;
        _stkSize--;
        return num;
    }

    /* 访问栈顶元素 */
    public int? Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return _stackPeek?.val;
    }

    /* 将 List 转化为 Array 并返回 */
    public int[] ToArray()
    {
        if (_stackPeek == null)
            return [];

        ListNode? node = _stackPeek;
        var res = new int[Size()];
        for (var i = res.Length - 1; i >= 0; i--)
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
    List<int> _stack;

    public ArrayStack()
    {
        // 初始化列表（动态数组）
        _stack = [];
    }

    /* 获取栈的长度 */
    public int Size()
    {
        return _stack.Count;
    }

    /* 判断栈是否为空 */
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入栈 */
    public void Push(int num)
    {
        _stack.Add(num);
    }

    /* 出栈 */
    public int Pop()
    {
        if (IsEmpty())
            throw new Exception();
        var val = Peek();
        _stack.RemoveAt(Size() - 1);
        return val;
    }

    /* 访问栈顶元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return _stack[Size() - 1];
    }

    /* 将 List 转化为 Array 并返回 */
    public int[] ToArray()
    {
        return [.. _stack];
    }
}

/* 基于链表实现的队列 */
public class LinkedListQueue
{
    ListNode? _front, _rear; // 头节点 front ，尾节点 rear
    int _queSize = 0;

    public LinkedListQueue()
    {
        _front = null;
        _rear = null;
    }

    /* 获取队列的长度 */
    public int Size()
    {
        return _queSize;
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
        if (_front == null)
        {
            _front = node;
            _rear = node;
            // 如果队列不为空，则将该节点添加到尾节点后
        }
        else if (_rear != null)
        {
            _rear.next = node;
            _rear = node;
        }

        _queSize++;
    }

    /* 出队 */
    public int Pop()
    {
        var num = Peek();
        // 删除头节点
        _front = _front?.next;
        _queSize--;
        return num;
    }

    /* 访问队首元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return _front!.val;
    }

    /* 将链表转化为 Array 并返回 */
    public int[] ToArray()
    {
        if (_front == null)
            return [];

        ListNode? node = _front;
        var res = new int[Size()];
        for (var i = 0; i < res.Length; i++)
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
    int[] _nums; // 用于存储队列元素的数组
    int _front; // 队首指针，指向队首元素
    int _queSize; // 队列长度

    public ArrayQueue(int capacity)
    {
        _nums = new int[capacity];
        _front = _queSize = 0;
    }

    /* 获取队列的容量 */
    int Capacity()
    {
        return _nums.Length;
    }

    /* 获取队列的长度 */
    public int Size()
    {
        return _queSize;
    }

    /* 判断队列是否为空 */
    public bool IsEmpty()
    {
        return _queSize == 0;
    }

    /* 入队 */
    public void Push(int num)
    {
        if (_queSize == Capacity())
        {
            Console.WriteLine("队列已满");
            return;
        }

        // 计算队尾指针，指向队尾索引 + 1
        // 通过取余操作实现 rear 越过数组尾部后回到头部
        var rear = ( _front + _queSize ) % Capacity();
        // 将 num 添加至队尾
        _nums[rear] = num;
        _queSize++;
    }

    /* 出队 */
    public int Pop()
    {
        var num = Peek();
        // 队首指针向后移动一位，若越过尾部，则返回到数组头部
        _front = ( _front + 1 ) % Capacity();
        _queSize--;
        return num;
    }

    /* 访问队首元素 */
    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return _nums[_front];
    }

    /* 返回数组 */
    public int[] ToArray()
    {
        // 仅转换有效长度范围内的列表元素
        var res = new int[_queSize];
        for (int i = 0, j = _front; i < _queSize; i++, j++)
        {
            res[i] = _nums[j % this.Capacity()];
        }

        return res;
    }
}

/* 双向链表节点 */
class BiListNode(int val)
{
    public int Val = val; // 节点值
    public BiListNode? Next = null; // 后继节点引用
    public BiListNode? Prev = null; // 前驱节点引用
}

/* 基于双向链表实现的双向队列 */
public class BiLinkedListDeque
{
    BiListNode? _front, _rear; // 头节点 front, 尾节点 rear
    int _queSize = 0; // 双向队列的长度

    public BiLinkedListDeque()
    {
        _front = null;
        _rear = null;
    }

    /* 获取双向队列的长度 */
    public int Size()
    {
        return _queSize;
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
            _front = node;
            _rear = node;
        }
        // 队首入队操作
        else if (isFront)
        {
            // 将 node 添加至链表头部
            _front!.Prev = node;
            node.Next = _front;
            _front = node; // 更新头节点
        }
        // 队尾入队操作
        else
        {
            // 将 node 添加至链表尾部
            _rear!.Next = node;
            node.Prev = _rear;
            _rear = node; // 更新尾节点
        }

        _queSize++; // 更新队列长度
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
            val = _front?.Val; // 暂存头节点值
            // 删除头节点
            BiListNode? fNext = _front?.Next;
            if (fNext != null)
            {
                fNext.Prev = null;
                _front!.Next = null;
            }

            _front = fNext; // 更新头节点
        }
        // 队尾出队操作
        else
        {
            val = _rear?.Val; // 暂存尾节点值
            // 删除尾节点
            BiListNode? rPrev = _rear?.Prev;
            if (rPrev != null)
            {
                rPrev.Next = null;
                _rear!.Prev = null;
            }

            _rear = rPrev; // 更新尾节点
        }

        _queSize--; // 更新队列长度
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
        return _front?.Val;
    }

    /* 访问队尾元素 */
    public int? PeekLast()
    {
        if (IsEmpty())
            throw new Exception();
        return _rear?.Val;
    }

    /* 返回数组用于打印 */
    public int?[] ToArray()
    {
        BiListNode? node = _front;
        var res = new int?[Size()];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = node?.Val;
            node = node?.Next;
        }

        return res;
    }
}

/* 基于环形数组实现的双向队列 */
public class ArrayDeque
{
    int[] _nums; // 用于存储双向队列元素的数组
    int _front; // 队首指针，指向队首元素
    int _queSize; // 双向队列长度

    /* 构造方法 */
    public ArrayDeque(int capacity)
    {
        _nums = new int[capacity];
        _front = _queSize = 0;
    }

    /* 获取双向队列的容量 */
    int Capacity()
    {
        return _nums.Length;
    }

    /* 获取双向队列的长度 */
    public int Size()
    {
        return _queSize;
    }

    /* 判断双向队列是否为空 */
    public bool IsEmpty()
    {
        return _queSize == 0;
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
        if (_queSize == Capacity())
        {
            Console.WriteLine("双向队列已满");
            return;
        }

        // 队首指针向左移动一位
        // 通过取余操作实现 front 越过数组头部后回到尾部
        _front = Index(_front - 1);
        // 将 num 添加至队首
        _nums[_front] = num;
        _queSize++;
    }

    /* 队尾入队 */
    public void PushLast(int num)
    {
        if (_queSize == Capacity())
        {
            Console.WriteLine("双向队列已满");
            return;
        }

        // 计算队尾指针，指向队尾索引 + 1
        var rear = Index(_front + _queSize);
        // 将 num 添加至队尾
        _nums[rear] = num;
        _queSize++;
    }

    /* 队首出队 */
    public int PopFirst()
    {
        var num = PeekFirst();
        // 队首指针向后移动一位
        _front = Index(_front + 1);
        _queSize--;
        return num;
    }

    /* 队尾出队 */
    public int PopLast()
    {
        var num = PeekLast();
        _queSize--;
        return num;
    }

    /* 访问队首元素 */
    public int PeekFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }

        return _nums[_front];
    }

    /* 访问队尾元素 */
    public int PeekLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }

        // 计算尾元素索引
        var last = Index(_front + _queSize - 1);
        return _nums[last];
    }

    /* 返回数组用于打印 */
    public int[] ToArray()
    {
        // 仅转换有效长度范围内的列表元素
        var res = new int[_queSize];
        for (int i = 0, j = _front; i < _queSize; i++, j++)
        {
            res[i] = _nums[Index(j)];
        }

        return res;
    }
}
