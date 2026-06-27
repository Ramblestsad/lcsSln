namespace Scratch.Labuladong.Algorithms.MaximumFrequencyStack;

// 895. Maximum Frequency Stack (Hard)
//
// Design a stack-like data structure to push elements to the stack and pop the most frequent
// element from the stack.
//
// Implement the FreqStack class:
//
// - FreqStack() constructs an empty frequency stack.
//
// - void push(int val) pushes an integer val onto the top of the stack.
//
// - int pop() removes and returns the most frequent element in the stack.
//
// - If there is a tie for the most frequent element, the element closest to the stack's top is
// removed and returned.
//
// Example 1:
//
// Input
// ["FreqStack", "push", "push", "push", "push", "push", "push", "pop", "pop", "pop", "pop"]
// [[], [5], [7], [5], [7], [4], [5], [], [], [], []]
// Output
// [null, null, null, null, null, null, null, 5, 7, 5, 4]
//
// Explanation
// FreqStack freqStack = new FreqStack();
// freqStack.push(5); // The stack is [5]
// freqStack.push(7); // The stack is [5,7]
// freqStack.push(5); // The stack is [5,7,5]
// freqStack.push(7); // The stack is [5,7,5,7]
// freqStack.push(4); // The stack is [5,7,5,7,4]
// freqStack.push(5); // The stack is [5,7,5,7,4,5]
// freqStack.pop(); // return 5, as 5 is the most frequent. The stack becomes [5,7,5,7,4].
// freqStack.pop(); // return 7, as 5 and 7 is the most frequent, but 7 is closest to the top. The
// stack becomes [5,7,5,4].
// freqStack.pop(); // return 5, as 5 is the most frequent. The stack becomes [5,7,4].
// freqStack.pop(); // return 4, as 4, 5 and 7 is the most frequent, but 4 is closest to the top.
// The stack becomes [5,7].
//
// Constraints:
//
// - 0 <= val <= 10^9
//
// - At most 2 * 10^4 calls will be made to push and pop.
//
// - It is guaranteed that there will be at least one element in the stack before calling pop.
//
// Related Topics: Hash Table, Stack, Design, Ordered Set

//leetcode submit region begin(Prohibit modification and deletion)
public class FreqStack
{
    /// 记录 FreqStack 中元素的最大频率
    int maxFreq = 0;

    // 记录 FreqStack 中每个 val 对应的出现频率，后文就称为 VF 表
    Dictionary<int, int> valToFreq = new();

    // 记录频率 freq 对应的 val 列表，后文就称为 FV 表
    Dictionary<int, Stack<int>> freqToVals = new();

    public void Push(int val)
    {
        // 修改 VF 表：val 对应的 freq 加一
        valToFreq.TryGetValue(val, out var vFreq);
        vFreq += 1;
        valToFreq[val] = vFreq;

        // 修改 FV 表：在 freq 对应的列表加上 val
        if (!freqToVals.TryGetValue(vFreq, out var l))
        {
            l = new Stack<int>();
            freqToVals[vFreq] = l;
        }

        l.Push(val);

        // 更新 maxFreq
        maxFreq = Math.Max(maxFreq, vFreq);
    }

    public int Pop()
    {
        // 修改 FV 表：pop 出一个 maxFreq 对应的元素 v
        if (!freqToVals.TryGetValue(maxFreq, out var maxVals))
            throw new InvalidOperationException("FreqStack is empty.");
        var toRm = maxVals.Pop();

        // 修改 VF 表：v 对应的 freq 减一
        if (valToFreq.TryGetValue(toRm, out var freq)) freq -= 1;
        valToFreq[toRm] = freq;

        // 更新 maxFreq
        if (maxVals.Count == 0)
        {
            // 如果 maxFreq 对应的元素空了
            maxFreq--;
        }

        return toRm;
    }
}

/**
 * Your FreqStack object will be instantiated and called as such:
 * FreqStack obj = new FreqStack();
 * obj.Push(val);
 * int param_2 = obj.Pop();
 */
//leetcode submit region end(Prohibit modification and deletion)
