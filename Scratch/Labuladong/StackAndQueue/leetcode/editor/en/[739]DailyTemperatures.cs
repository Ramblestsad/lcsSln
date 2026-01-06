namespace Scratch.Labuladong.Algorithms.DailyTemperatures;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        var n = temperatures.Length;
        var res = new int[n];

        // 这里放元素索引，而不是元素
        var s = new Stack<int>();
        for (int i = n - 1; i >= 0; i--)
        {
            while (s.Count != 0 && temperatures[s.Peek()] <= temperatures[i])
            {
                s.Pop();
            }

            // 得到索引间距
            res[i] = s.Count == 0 ? 0 : ( s.Peek() - i );
            s.Push(i);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
