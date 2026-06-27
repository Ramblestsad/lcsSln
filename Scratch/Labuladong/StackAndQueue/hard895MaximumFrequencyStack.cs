namespace Scratch.Labuladong.Algorithms.MaximumFrequencyStack;

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
