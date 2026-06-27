namespace Scratch.Labuladong.Algorithms.DesignFrontMiddleBackQueue;

// 1670. Design Front Middle Back Queue (Medium)
//
// Design a queue that supports push and pop operations in the front, middle, and back.
//
// Implement the FrontMiddleBack class:
//
// - FrontMiddleBack() Initializes the queue.
//
// - void pushFront(int val) Adds val to the front of the queue.
//
// - void pushMiddle(int val) Adds val to the middle of the queue.
//
// - void pushBack(int val) Adds val to the back of the queue.
//
// - int popFront() Removes the front element of the queue and returns it. If the queue is empty,
// return -1.
//
// - int popMiddle() Removes the middle element of the queue and returns it. If the queue is empty,
// return -1.
//
// - int popBack() Removes the back element of the queue and returns it. If the queue is empty,
// return -1.
//
// Notice that when there are two middle position choices, the operation is performed on the
// frontmost middle position choice. For example:
//
// - Pushing 6 into the middle of [1, 2, 3, 4, 5] results in [1, 2, 6, 3, 4, 5].
//
// - Popping the middle from [1, 2, 3, 4, 5, 6] returns 3 and results in [1, 2, 4, 5, 6].
//
// Example 1:
//
// Input:
// ["FrontMiddleBackQueue", "pushFront", "pushBack", "pushMiddle", "pushMiddle", "popFront",
// "popMiddle", "popMiddle", "popBack", "popFront"]
// [[], [1], [2], [3], [4], [], [], [], [], []]
// Output:
// [null, null, null, null, null, 1, 3, 4, 2, -1]
//
// Explanation:
// FrontMiddleBackQueue q = new FrontMiddleBackQueue();
// q.pushFront(1); // [1]
// q.pushBack(2); // [1, 2]
// q.pushMiddle(3); // [1, 3, 2]
// q.pushMiddle(4); // [1, 4, 3, 2]
// q.popFront(); // return 1 -> [4, 3, 2]
// q.popMiddle(); // return 3 -> [4, 2]
// q.popMiddle(); // return 4 -> [2]
// q.popBack(); // return 2 -> []
// q.popFront(); // return -1 -> [] (The queue is empty)
//
// Constraints:
//
// - 1 <= val <= 10^9
//
// - At most 1000 calls will be made to pushFront, pushMiddle, pushBack, popFront, popMiddle, and
// popBack.
//
// Related Topics: Array, Linked List, Design, Queue, Doubly-Linked List, Data Stream

//leetcode submit region begin(Prohibit modification and deletion)
public class FrontMiddleBackQueue
{
    // 用两个列表表示队列的左右两部分，以便操作中间元素
    private LinkedList<int> _left = new(); // 少
    private LinkedList<int> _right = new(); // 多

    // 如果是奇数个元素，维护左边少右边多，所以：
    // 1、如果有偶数个元素时，pushMiddle 优先向右边添加
    // 2、如果有奇数个元素时，popMiddle 优先从右边删除
    // 3、如果只有 1 个元素，popFront 的时候，要去右边删除

    // 维护左边少右边多的状态，每次增删元素之后都要执行一次
    private void Balance()
    {
        // 右边最多比左边多一个元素
        if (_right.Count > _left.Count + 1)
        {
            // 右边多，匀一个给左边
            _left.AddLast(_right.First!.Value);
            _right.RemoveFirst();
        }

        if (_left.Count > _right.Count)
        {
            // 左边多，匀一个给右边
            _right.AddFirst(_left.Last!.Value);
            _left.RemoveLast();
        }
    }

    public void PushFront(int val)
    {
        // 无脑往逻辑上的最前面加，每次加完都Balance
        _left.AddFirst(val);
        Balance();
    }

    public void PushMiddle(int val)
    {
        if (Size() % 2 == 0)
        {
            // 如果有偶数个元素时，pushMiddle 优先向右边添加
            _right.AddFirst(val);
        }
        else
        {
            _left.AddLast(val);
        }

        Balance();
    }

    public void PushBack(int val)
    {
        // 无脑往逻辑上的最后面加，每次加完都Balance
        _right.AddLast(val);
        Balance();
    }

    public int PopFront()
    {
        if (Size() == 0) return -1;
        if (Size() == 1)
        {
            // 如果只有 1 个元素，popFront 的时候，要去右边删除
            var v = _right.First!.Value;
            _right.RemoveFirst();
            return v;
        }

        var vv = _left.First!.Value;
        _left.RemoveFirst();
        Balance();
        return vv;
    }

    public int PopMiddle()
    {
        if (Size() == 0) return -1;
        int e;
        if (Size() % 2 == 0)
        {
            e = _left.Last!.Value;
            _left.RemoveLast();
        }
        else
        {
            // 如果有奇数个元素时，popMiddle 优先从右边删除
            e = _right.First!.Value;
            _right.RemoveFirst();
        }

        Balance();
        return e;
    }

    public int PopBack()
    {
        if (Size() == 0) return -1;
        var e = _right.Last!.Value;
        _right.RemoveLast();
        Balance();
        return e;
    }

    public int Size()
    {
        return _left.Count + _right.Count;
    }
}
/**
 * Your FrontMiddleBackQueue object will be instantiated and called as such:
 * FrontMiddleBackQueue obj = new FrontMiddleBackQueue();
 * obj.PushFront(val);
 * obj.PushMiddle(val);
 * obj.PushBack(val);
 * int param_4 = obj.PopFront();
 * int param_5 = obj.PopMiddle();
 * int param_6 = obj.PopBack();
 */
//leetcode submit region end(Prohibit modification and deletion)
