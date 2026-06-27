namespace Scratch.Labuladong.Algorithms.MinStack;

//leetcode submit region begin(Prohibit modification and deletion)
public class MinStack
{
    // 记录栈中的所有元素
    private Stack<int> _stk = new();

    // 阶段性记录栈中的最小元素
    private Stack<int> _minStk = new();

    public void Push(int val)
    {
        _stk.Push(val);
        // 维护 minStk 栈顶为全栈最小元素
        if (_minStk.Count == 0 || val <= _minStk.Peek())
        {
            // 新插入的这个元素就是全栈最小的
            _minStk.Push(val);
        }
        else
        {
            // 插入的这个元素比较大, 那就再插入一次最小元素
            _minStk.Push(_minStk.Peek());
        }
    }

    public void Pop()
    {
        _stk.Pop();
        _minStk.Pop();
    }

    public int Top()
    {
        return _stk.Peek();
    }

    public int GetMin()
    {
        // minStk 栈顶为全栈最小元素
        return _minStk.Peek();
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(val);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */
//leetcode submit region end(Prohibit modification and deletion)
