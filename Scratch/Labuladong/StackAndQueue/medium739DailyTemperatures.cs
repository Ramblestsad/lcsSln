namespace Scratch.Labuladong.Algorithms.DailyTemperatures;

// 739. Daily Temperatures (Medium)
//
// Given an array of integers temperatures represents the daily temperatures, return an array
// answer such that answer[i] is the number of days you have to wait after the i^th day to get a
// warmer temperature. If there is no future day for which this is possible, keep answer[i] == 0
// instead.
//
// Example 1:
//
// Input: temperatures = [73,74,75,71,69,72,76,73]
// Output: [1,1,4,2,1,1,0,0]
// Example 2:
//
// Input: temperatures = [30,40,50,60]
// Output: [1,1,1,0]
// Example 3:
//
// Input: temperatures = [30,60,90]
// Output: [1,1,0]
//
// Constraints:
//
// - 1 <= temperatures.length <= 10^5
//
// - 30 <= temperatures[i] <= 100
//
// Related Topics: Array, Stack, Monotonic Stack

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
