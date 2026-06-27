namespace Scratch.Labuladong.Algorithms.MinStack;

// 155. Min Stack (Medium)
//
// Design a stack that supports push, pop, top, and retrieving the minimum element in constant
// time.
//
// Implement the MinStack class:
//
// - MinStack() initializes the stack object.
//
// - void push(int value) pushes the element value onto the stack.
//
// - void pop() removes the element on the top of the stack.
//
// - int top() gets the top element of the stack.
//
// - int getMin() retrieves the minimum element in the stack.
//
// You must implement a solution with O(1) time complexity for each function.
//
// Example 1:
//
// Input
// ["MinStack","push","push","push","getMin","pop","top","getMin"]
// [[],[-2],[0],[-3],[],[],[],[]]
//
// Output
// [null,null,null,null,-3,null,0,-2]
//
// Explanation
// MinStack minStack = new MinStack();
// minStack.push(-2);
// minStack.push(0);
// minStack.push(-3);
// minStack.getMin(); // return -3
// minStack.pop();
// minStack.top(); // return 0
// minStack.getMin(); // return -2
//
// Constraints:
//
// - -2^31 <= val <= 2^31 - 1
//
// - Methods pop, top and getMin operations will always be called on non-empty stacks.
//
// - At most 3 * 10^4 calls will be made to push, pop, top, and getMin.
//
// Related Topics: Stack, Design

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
