namespace Scratch.Labuladong.Algorithms.ImplementStackUsingQueues;

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
