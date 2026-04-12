/*
 * @lc app=leetcode id=300 lang=csharp
 * @lcpr version=30402
 *
 * [300] Longest Increasing Subsequence
 */

namespace Scratch.Labuladong.Algorithms.LongestIncreasingSubsequence;

//Given an integer array nums, return the length of the longest strictly
//increasing subsequence.
//
//
// Example 1:
//
//
//Input: nums = [10,9,2,5,3,7,101,18]
//Output: 4
//Explanation: The longest increasing subsequence is [2,3,7,101], therefore the
//length is 4.
//
//
// Example 2:
//
//
//Input: nums = [0,1,0,3,2,3]
//Output: 4
//
//
// Example 3:
//
//
//Input: nums = [7,7,7,7,7,7,7]
//Output: 1
//
//
//
// Constraints:
//
//
// 1 <= nums.length <= 2500
// -10⁴ <= nums[i] <= 10⁴
//
//
//
// Follow up: Can you come up with an algorithm that runs in O(n log(n)) time
//complexity?
//
// Related TopicsArray | Binary Search | Dynamic Programming
//
// 👍 22669, 👎 511bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    // dp[i] 表示以 nums[i] 这个数结尾的最长递增子序列的长度。
    // base case：dp[i] 初始值为 1，因为以 nums[i] 结尾的最长递增子序列起码要包含它自己。
    // 最终结果（子序列的最大长度）应该是 dp 数组中的最大值。
    // O(n^2)
    public int LengthOfLIS(int[] nums)
    {
        var dp = new int[nums.Length];
        Array.Fill(dp, 1);

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (nums[j] < nums[i])
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
            }
        }

        var res = 0;
        foreach (int t in dp)
        {
            res = Math.Max(res, t);
        }

        return res;
    }
}
// @lc code=end
