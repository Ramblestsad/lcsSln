/*
 * @lc app=leetcode id=41 lang=csharp
 * @lcpr version=30403
 *
 * [41] First Missing Positive
 */

namespace Scratch.Labuladong.Algorithms.FirstMissingPositive;

// @lc code=start
public class Solution
{
    public int FirstMissingPositive(int[] nums)
    {
        var n = nums.Length;
        // 第一步：把无效数字（非正数或超范围）替换成哨兵
        for (var i = 0; i < n; i++)
        {
            if (nums[i] <= 0 || nums[i] > n)
            {
                nums[i] = n + 1;
            }
        }

        // 第二步：用符号位标记 x 出现过，把 nums[x-1] 取反
        for (var i = 0; i < n; i++)
        {
            var x = Math.Abs(nums[i]);
            if (x <= n)
            {
                nums[x - 1] = -Math.Abs(nums[x - 1]);
            }
        }

        // 第三步：第一个正数所在下标 i，说明 i+1 缺失
        for (var i = 0; i < n; i++)
        {
            if (nums[i] > 0)
            {
                return i + 1;
            }
        }

        return n + 1;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [1,2,0]\n
// @lcpr case=end

// @lcpr case=start
// [3,4,-1,1]\n
// @lcpr case=end

// @lcpr case=start
// [7,8,9,11,12]\n
// @lcpr case=end
 */
