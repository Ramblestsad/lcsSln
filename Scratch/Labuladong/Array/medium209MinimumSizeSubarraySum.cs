/*
 * @lc app=leetcode id=209 lang=csharp
 * @lcpr version=30402
 *
 * [209] Minimum Size Subarray Sum
 */

namespace Scratch.Labuladong.Algorithms.MinimumSizeSubarraySum;

// @lc code=start
public class Solution
{
    public int MinSubArrayLen(int target, int[] nums)
    {
        int left = 0, right = 0;
        var windowSum = 0;
        var res = int.MaxValue;

        while (right < nums.Length)
        {
            // 扩大窗口
            windowSum += nums[right];
            right++;

            while (windowSum >= target && left < right)
            {
                // 已经达到target，缩小窗口，同时更新结果
                res = Math.Min(res, right - left);
                windowSum -= nums[left];
                left++;
            }
        }

        return res == int.MaxValue ? 0 : res;
    }
}
// @lc code=end
