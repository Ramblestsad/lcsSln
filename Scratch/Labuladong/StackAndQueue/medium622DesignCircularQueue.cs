namespace Scratch.Labuladong.Algorithms.DesignCircularQueue;

// 622. Design Circular Queue (Medium)
//
// Design your implementation of the circular queue. The circular queue is a linear data structure
// in which the operations are performed based on FIFO (First In First Out) principle, and the last
// position is connected back to the first position to make a circle. It is also called "Ring
// Buffer".
//
// One of the benefits of the circular queue is that we can make use of the spaces in front of the
// queue. In a normal queue, once the queue becomes full, we cannot insert the next element even if
// there is a space in front of the queue. But using the circular queue, we can use the space to
// store new values.
//
// Implement the MyCircularQueue class:
//
// - MyCircularQueue(k) Initializes the object with the size of the queue to be k.
//
// - int Front() Gets the front item from the queue. If the queue is empty, return -1.
//
// - int Rear() Gets the last item from the queue. If the queue is empty, return -1.
//
// - boolean enQueue(int value) Inserts an element into the circular queue. Return true if the
// operation is successful.
//
// - boolean deQueue() Deletes an element from the circular queue. Return true if the operation is
// successful.
//
// - boolean isEmpty() Checks whether the circular queue is empty or not.
//
// - boolean isFull() Checks whether the circular queue is full or not.
//
// You must solve the problem without using the built-in queue data structure in your programming
// language.
//
// Example 1:
//
// Input
// ["MyCircularQueue", "enQueue", "enQueue", "enQueue", "enQueue", "Rear", "isFull", "deQueue",
// "enQueue", "Rear"]
// [[3], [1], [2], [3], [4], [], [], [], [4], []]
// Output
// [null, true, true, true, false, 3, true, true, true, 4]
//
// Explanation
// MyCircularQueue myCircularQueue = new MyCircularQueue(3);
// myCircularQueue.enQueue(1); // return True
// myCircularQueue.enQueue(2); // return True
// myCircularQueue.enQueue(3); // return True
// myCircularQueue.enQueue(4); // return False
// myCircularQueue.Rear(); // return 3
// myCircularQueue.isFull(); // return True
// myCircularQueue.deQueue(); // return True
// myCircularQueue.enQueue(4); // return True
// myCircularQueue.Rear(); // return 4
//
// Constraints:
//
// - 1 <= k <= 1000
//
// - 0 <= value <= 1000
//
// - At most 3000 calls will be made to enQueue, deQueue, Front, Rear, isEmpty, and isFull.
//
// Related Topics: Array, Linked List, Design, Queue

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
