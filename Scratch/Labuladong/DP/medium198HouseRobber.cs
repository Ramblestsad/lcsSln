/*
 * @lc app=leetcode id=198 lang=csharp
 * @lcpr version=30403
 *
 * [198] House Robber
 */

namespace Scratch.Labuladong.Algorithms.HouseRobber;

// @lc code=start
public class Solution
{
    private int[] memo = [];

    public int Rob(int[] nums)
    {
        memo = new int[nums.Length];
        Array.Fill(memo, -1);

        // 强盗从第 0 间房子开始抢劫
        return dp(nums, 0);
    }

    // 返回 dp[start..] 能抢到的最大值
    private int dp(int[] nums, int start)
    {
        if (start >= nums.Length)
        {
            return 0;
        }

        // 避免重复计算
        if (memo[start] != -1) return memo[start];

        var res = Math.Max(
            dp(nums, start + 1),
            nums[start] + dp(nums, start + 2)
        );
        // 记入备忘录
        memo[start] = res;
        return res;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [1,2,3,1]\n
// @lcpr case=end

// @lcpr case=start
// [2,7,9,3,1]\n
// @lcpr case=end
 */
