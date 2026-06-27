namespace Scratch.Labuladong.Algorithms.DesignCircularQueue;

//leetcode submit region begin(Prohibit modification and deletion)
public class MyCircularQueue
{
    private ArrayQueue<int> q;
    private int maxCap;

    public MyCircularQueue(int k)
    {
        q = new ArrayQueue<int>(k);
        maxCap = k;
    }

    public bool EnQueue(int value)
    {
        if (q.Size() == maxCap) return false;

        q.Enqueue(value);

        return true;
    }

    public bool DeQueue()
    {
        if (q.IsEmpty()) return false;

        q.Dequeue();

        return true;
    }

    public int Front()
    {
        if (q.IsEmpty()) return -1;

        return q.PeekFirst();
    }

    public int Rear()
    {
        if (q.IsEmpty()) return -1;

        return q.PeekLast();
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

public class ArrayQueue<T>
{
    private int _size;
    private T[] _data;
    private const int s_initCap = 2;

    private int _front, _rear;

    public ArrayQueue(int k)
    {
        if (k < 0) throw new ArgumentOutOfRangeException();
        _size = 0;
        _data = new T[k];
        _front = 0;
        _rear = 0;
    }

    public ArrayQueue(): this(s_initCap)
    {
    }

    public void Enqueue(T val)
    {
        if (_size == _data.Length) Resize(_size * 2);

        _data[_rear] = val;
        _rear++;
        if (_rear == _data.Length) _rear = 0; // 这就叫环

        _size++;
    }

    public T Dequeue()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty queue.");

        if (_size == _data.Length / 4) Resize(_data.Length / 2);

        var oldVal = _data[_front];
        _data[_front] = default!;
        _front++;

        if (_front == _data.Length) _front = 0;

        _size--;

        return oldVal;
    }

    private void Resize(int newCap)
    {
        T[] temp = new T[newCap];

        // first ----- last
        // --- last    first ---

        for (int i = 0; i < _size; i++)
        {
            temp[i] = _data[( _front + i ) % _data.Length];
        }

        _front = 0;
        _rear = _size;
        _data = temp;
    }

    public T PeekFirst()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty queue.");

        return _data[_front];
    }

    public T PeekLast()
    {
        if (IsEmpty()) throw new InvalidOperationException("Empty queue.");

        // 回环首了，返回原数组的最后一个元素
        if (_rear == 0) return _data[^1];

        return _data[_rear - 1];
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
 * Your MyCircularQueue object will be instantiated and called as such:
 * MyCircularQueue obj = new MyCircularQueue(k);
 * bool param_1 = obj.EnQueue(value);
 * bool param_2 = obj.DeQueue();
 * int param_3 = obj.Front();
 * int param_4 = obj.Rear();
 * bool param_5 = obj.IsEmpty();
 * bool param_6 = obj.IsFull();
 */
//leetcode submit region end(Prohibit modification and deletion)
