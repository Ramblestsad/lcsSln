namespace Scratch.Labuladong.Algorithms.ImplementStackUsingQueues;

// 225. Implement Stack using Queues (Easy)
//
// Implement a last-in-first-out (LIFO) stack using only two queues. The implemented stack should
// support all the functions of a normal stack (push, top, pop, and empty).
//
// Implement the MyStack class:
//
// - void push(int x) Pushes element x to the top of the stack.
//
// - int pop() Removes the element on the top of the stack and returns it.
//
// - int top() Returns the element on the top of the stack.
//
// - boolean empty() Returns true if the stack is empty, false otherwise.
//
// Notes:
//
// - You must use only standard operations of a queue, which means that only push to back, peek/pop
// from front, size and is empty operations are valid.
//
// - Depending on your language, the queue may not be supported natively. You may simulate a queue
// using a list or deque (double-ended queue) as long as you use only a queue's standard
// operations.
//
// Example 1:
//
// Input
// ["MyStack", "push", "push", "top", "pop", "empty"]
// [[], [1], [2], [], [], []]
// Output
// [null, null, null, 2, 2, false]
//
// Explanation
// MyStack myStack = new MyStack();
// myStack.push(1);
// myStack.push(2);
// myStack.top(); // return 2
// myStack.pop(); // return 2
// myStack.empty(); // return False
//
// Constraints:
//
// - 1 <= x <= 9
//
// - At most 100 calls will be made to push, pop, top, and empty.
//
// - All the calls to pop and top are valid.
//
// Follow-up: Can you implement the stack using only one queue?
//
// Related Topics: Stack, Design, Queue

//leetcode submit region begin(Prohibit modification and deletion)
public class MyStack
{
    private Queue<int> q = new();
    private int topElem = 0;

    public void Push(int x)
    {
        // x是队列的尾，栈的顶
        q.Enqueue(x);
        topElem = x;
    }

    public int Pop()
    {
        var size = q.Count;
        // 留下队尾 2 个元素
        while (size > 2)
        {
            q.Enqueue(q.Dequeue());
            size--;
        }

        // 记录弹出desired tail后新的队尾元素
        topElem = q.Peek();
        q.Enqueue(q.Dequeue());

        // 删除之前想要的队尾元素
        return q.Dequeue();
    }

    public int Top()
    {
        return topElem;
    }

    public bool Empty()
    {
        return q.Count == 0;
    }
}

/**
 * Your MyStack object will be instantiated and called as such:
 * MyStack obj = new MyStack();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Top();
 * bool param_4 = obj.Empty();
 */
//leetcode submit region end(Prohibit modification and deletion)
