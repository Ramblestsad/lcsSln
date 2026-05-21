/*
 * @lc app=leetcode id=136 lang=csharp
 * @lcpr version=30403
 *
 * [136] Single Number
 */

namespace Scratch.Labuladong.Algorithms.SingleNumber;

// @lc code=start
public class Solution
{
    public int SingleNumber(int[] nums)
    {
        var res = 0;

        foreach (var num in nums)
        {
            res ^= num;
        }

        return res;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [2,2,1]\n
// @lcpr case=end

// @lcpr case=start
// [4,1,2,1,2]\n
// @lcpr case=end

// @lcpr case=start
// [1]\n
// @lcpr case=end
 */
