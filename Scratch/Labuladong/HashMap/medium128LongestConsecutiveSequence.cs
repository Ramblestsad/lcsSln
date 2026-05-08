/*
 * @lc app=leetcode id=128 lang=csharp
 * @lcpr version=30403
 *
 * [128] Longest Consecutive Sequence
 */

namespace Scratch.Labuladong.Algorithms.LongestConsecutiveSeq;

// @lc code=start
public class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        var set = new HashSet<int>();

        foreach (var num in nums)
        {
            set.Add(num);
        }

        var res = 0;

        foreach (var num in set)
        {
            // num 不是子序列的第一个（最小）
            if (set.Contains(num - 1)) continue;

            // num 是子序列的第一个
            var curNum = num;
            var curLen = 1;

            while (set.Contains(curNum + 1))
            {
                curNum += 1;
                curLen += 1;
            }

            // update res
            res = Math.Max(res, curLen);
        }

        return res;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [100,4,200,1,3,2]\n
// @lcpr case=end

// @lcpr case=start
// [0,3,7,2,5,8,4,6,0,1]\n
// @lcpr case=end

// @lcpr case=start
// [1,0,1,2]\n
// @lcpr case=end
 */
