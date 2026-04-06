/*
 * @lc app=leetcode id=53 lang=csharp
 * @lcpr version=30402
 *
 * [53] Maximum Subarray
 */

namespace Scratch.Labuladong.Algorithms.MaximumSubarray;

//Given an integer array nums, find the subarray with the largest sum, and
//return its sum.
//
//
// Example 1:
//
//
//Input: nums = [-2,1,-3,4,-1,2,1,-5,4]
//Output: 6
//Explanation: The subarray [4,-1,2,1] has the largest sum 6.
//
//
// Example 2:
//
//
//Input: nums = [1]
//Output: 1
//Explanation: The subarray [1] has the largest sum 1.
//
//
// Example 3:
//
//
//Input: nums = [5,4,-1,7,8]
//Output: 23
//Explanation: The subarray [5,4,-1,7,8] has the largest sum 23.
//
//
//
// Constraints:
//
//
// 1 <= nums.length <= 10⁵
// -10⁴ <= nums[i] <= 10⁴
//
//
//
// Follow up: If you have figured out the O(n) solution, try coding another
//solution using the divide and conquer approach, which is more subtle.
//
// Related TopicsArray | Divide and Conquer | Dynamic Programming
//
// 👍 37620, 👎 1599bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int MaxSubArray(int[] nums)
    {
        return MaxSubArraySlidingWindow(nums);
    }

    private int MaxSubArraySlidingWindow(int[] nums)
    {
        var left = 0;
        var right = 0;
        var windowSum = 0;
        var maxSum = int.MinValue;

        while (right < nums.Length)
        {
            // 扩大窗口并更新窗口内的元素和
            windowSum += nums[right];
            right++;

            // 更新答案
            maxSum = windowSum > maxSum ? windowSum : maxSum;

            // 判断窗口是否要收缩
            // 由于 windowSum <0，所以一定有：
            //      windowSum + x < x
            // 任何以当前负和窗口为前缀的子数组，都不如直接从后面某个位置重新开始。
            // 所以当 windowSum <0 时，缩小窗口本质上是在丢弃无用前缀。
            while (windowSum < 0)
            {
                // 缩小窗口并更新窗口内的元素和
                windowSum -= nums[left];
                left++;
            }
        }

        return maxSum;
    }

    // 定义 dp 数组的含义：
    //      以 nums[i] 为结尾的「最大子数组和」为 dp[i]。
    // dp[i] 有两种「选择」：dp[i] = Math.Max(nums[i], nums[i] + dp[i - 1]);
    //      - 要么与前面的相邻子数组连接，形成一个和更大的子数组；
    //      - 要么不与前面的子数组连接，自成一派，自己作为一个子数组。
    private int MaxSubArrayDp(int[] nums)
    {
        var n = nums.Length;
        if (n == 0) return 0;
        var dp = new int[n];

        // base case
        // 第一个元素前面没有子数组
        dp[0] = nums[0];

        // 状态转移方程
        for (int i = 1; i < n; i++)
        {
            dp[i] = Math.Max(nums[i], nums[i] + dp[i - 1]);
        }

        // 得到 nums 的最大子数组
        var res = int.MinValue;
        for (int i = 0; i < n; i++)
        {
            res = Math.Max(res, dp[i]);
        }

        return res;
    }

    private int MaxSubArrayPreSum(int[] nums)
    {
        var n = nums.Length;
        var preSum = new int[n + 1];
        preSum[0] = 0;
        // 构造 nums 的前缀和数组
        for (int i = 1; i <= n; i++)
            preSum[i] = preSum[i - 1] + nums[i - 1];

        var res = int.MinValue;
        var minVal = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            // 维护 minVal 是 preSum[0..i] 的最小值
            minVal = Math.Min(minVal, preSum[i]);
            // 以 nums[i] 结尾的最大子数组和就是 preSum[i+1] - min(preSum[0..i])
            res = Math.Max(res, preSum[i + 1] - minVal);
        }

        return res;
    }
}
// @lc code=end
