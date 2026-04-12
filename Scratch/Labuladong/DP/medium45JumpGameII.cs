/*
 * @lc app=leetcode id=45 lang=csharp
 * @lcpr version=30402
 *
 * [45] Jump Game II
 */

namespace Scratch.Labuladong.Algorithms.JumpGameII;

//You are given a 0-indexed array of integers nums of length n. You are
//initially positioned at index 0.
//
// Each element nums[i] represents the maximum length of a forward jump from
//index i. In other words, if you are at index i, you can jump to any index (i + j)
//where:
//
//
// 0 <= j <= nums[i] and
// i + j < n
//
//
// Return the minimum number of jumps to reach index n - 1. The test cases are
//generated such that you can reach index n - 1.
//
//
// Example 1:
//
//
//Input: nums = [2,3,1,1,4]
//Output: 2
//Explanation: The minimum number of jumps to reach the last index is 2. Jump 1
//step from index 0 to 1, then 3 steps to the last index.
//
//
// Example 2:
//
//
//Input: nums = [2,3,0,1,4]
//Output: 2
//
//
//
// Constraints:
//
//
// 1 <= nums.length <= 10⁴
// 0 <= nums[i] <= 1000
// It's guaranteed that you can reach nums[n - 1].
//
//
// Related TopicsArray | Dynamic Programming | Greedy
//
// 👍 16412, 👎 702bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int Jump(int[] nums)
    {
        var n = nums.Length;
        if (n <= 1)
        {
            return 0;
        }

        // jumps 步可以跳到索引区间 [i, end]
        var end = 0;
        var jumps = 0;
        // 在 [i, end] 区间内，最远可以跳到的索引是 farthest
        var farthest = 0;

        for (int i = 0; i < n - 1; i++)
        {
            // 计算从索引 i 可以跳到的最远索引
            farthest = Math.Max(farthest, i + nums[i]);

            if (i == end)
            {
                // [i, end] 区间是 jumps 步可达的索引范围
                // 现在已经遍历完 [i, end]，所以需要再跳一步
                jumps++;
                end = farthest;
                if (farthest >= n - 1) return jumps;
            }
        }

        return -1;
    }
}
// @lc code=end
