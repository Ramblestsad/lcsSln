/*
 * @lc app=leetcode id=503 lang=csharp
 * @lcpr version=30402
 *
 * [503] Next Greater Element II
 */

namespace Scratch.Labuladong.Algorithms.NextGreaterElementII;

// @lc code=start
public class Solution
{
    public int[] NextGreaterElements(int[] nums)
    {
        var n = nums.Length;
        var res = new int[n];

        var s = new Stack<int>();

        // 数组长度加倍模拟环形数组
        for (int i = 2 * n - 1; i >= 0; i--)
        {
            // 索引 i 要求模
            while (s.Count != 0 && s.Peek() <= nums[i % n])
            {
                s.Pop();
            }

            res[i % n] = s.Count == 0 ? -1 : s.Peek();
            s.Push(nums[i % n]);
        }

        return res;
    }
}
// @lc code=end
