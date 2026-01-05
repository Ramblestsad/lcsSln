namespace Scratch.Labuladong.Algorithms.DesignCircularDeque;

//leetcode submit region begin(Prohibit modification and deletion)
public class MyCircularDeque
{
    private ArrayDeque<int> q;
    private int maxCap;

    public MyCircularDeque(int k)
    {
        q = new ArrayDeque<int>(k);
        maxCap = k;
    }

    public bool InsertFront(int value)
    {
        if (q.Size() == maxCap) return false;

        q.AddFirst(value);
        return true;
    }

    public bool InsertLast(int value)
    {
        if (q.Size() == maxCap) return false;

        q.AddLast(value);
        return true;
    }

    public bool DeleteFront()
    {
        if (q.IsEmpty()) return false;

        q.RemoveFirst();
        return true;
    }

    public bool DeleteLast()
    {
        if (q.IsEmpty()) return false;

        q.RemoveLast();
        return true;
    }

    public int GetFront()
    {
        if (q.IsEmpty()) return -1;

        return q.GetFirst();
    }

    public int GetRear()
    {
        if (q.IsEmpty()) return -1;

        return q.GetLast();
    }

    public bool IsEmpty()
    {
        return q.IsEmpty();
    }

    public bool IsFull()
    {
        return q.Size() == maxCap;
    }
}

public class ArrayDeque<T>
{
    private int _size;
    private T[] _data;
    private const int initCap = 2;

    private int _front, _rear;

    public ArrayDeque(int k)
    {
        _size = 0;
        _data = new T[k];
        // _rear 是下一次应该添加元素的索引
        // _front----_rear, [_front, _rear)
        // 比如 _front = 1，_rear = 3，size = 2
        _front = _rear = 0;
    }

    public ArrayDeque(): this(initCap)
    {
    }

    public T GetFirst()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty deque.");

        return _data[_front];
    }

    public T GetLast()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty deque.");

        if (_rear == 0) return _data[^1];

        return _data[_rear - 1];
    }

    public void AddFirst(T val)
    {
        /*
         * 核心不在“插入”，而在于维护这份数组双端队列的*不变式*：
         * _front 指向“队首元素”的索引
         * _rear 指向“下一次在队尾插入的位置”（也就是区间 [_front, _rear) 的右开端）
         * _size 是当前元素数量
         */
        if (_size == _data.Length) Resize(_data.Length * 2);

        // 情况一：_front----_rear
        // 情况二：---_rear  _front---

        // 左移 _front
        if (_front == 0) // _front == 0 是一种特殊情况
        {
            _front = _data.Length - 1;
        }
        else
        {
            _front--;
        }

        // insert
        _data[_front] = val;
        _size++;
    }

    public void AddLast(T val)
    {
        if (_size == _data.Length) Resize(_data.Length * 2);

        // insert
        _data[_rear] = val;
        _rear++;

        if (_rear == _data.Length) _rear = 0;
        _size++;
    }

    public T RemoveFirst()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty deque.");
        if (_size == _data.Length / 4) Resize(_data.Length / 2);

        // _front++;
        var oldVal = _data[_front];
        _data[_front] = default!;
        _front++;

        if (_front == _data.Length) _front = 0;
        _size--;

        return oldVal;
    }

    public T RemoveLast()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty deque.");
        if (_size == _data.Length / 4) Resize(_data.Length / 2);

        // 情况一：_front----_rear
        // 情况二：---_rear  _front---

        // 左移 _rear
        if (_rear == 0) // _rear == 0 的时候是特殊情况
        {
            _rear = _data.Length - 1;
        }
        else
        {
            _rear--;
        }

        var oldVal = _data[_rear];
        _data[_rear] = default!;

        _size--;

        return oldVal;
    }

    private void Resize(int newCap)
    {
        var temp = new T[newCap];

        // _front ----- _rear
        // --- _rear    _front ---

        for (int i = 0; i < _size; i++)
        {
            temp[i] = _data[( _front + i ) % _data.Length];
        }

        _front = 0;
        _rear = _size;
        _data = temp;
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public int Size()
    {
        return _size;
    }
}
/**
 * Your MyCircularDeque object will be instantiated and called as such:
 * MyCircularDeque obj = new MyCircularDeque(k);
 * bool param_1 = obj.InsertFront(value);
 * bool param_2 = obj.InsertLast(value);
 * bool param_3 = obj.DeleteFront();
 * bool param_4 = obj.DeleteLast();
 * int param_5 = obj.GetFront();
 * int param_6 = obj.GetRear();
 * bool param_7 = obj.IsEmpty();
 * bool param_8 = obj.IsFull();
 */
//leetcode submit region end(Prohibit modification and deletion)
