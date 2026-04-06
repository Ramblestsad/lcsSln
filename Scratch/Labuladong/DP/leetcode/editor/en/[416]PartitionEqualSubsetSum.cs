/*
 * @lc app=leetcode id=416 lang=csharp
 * @lcpr version=30402
 *
 * [416] Partition Equal Subset Sum
 */

namespace Scratch.Labuladong.Algorithms.PartitionEqualSubsetSum;

//Given an integer array nums, return true if you can partition the array into
//two subsets such that the sum of the elements in both subsets is equal or false
//otherwise.
//
//
// Example 1:
//
//
//Input: nums = [1,5,11,5]
//Output: true
//Explanation: The array can be partitioned as [1, 5, 5] and [11].
//
//
// Example 2:
//
//
//Input: nums = [1,2,3,5]
//Output: false
//Explanation: The array cannot be partitioned into equal sum subsets.
//
//
//
// Constraints:
//
//
// 1 <= nums.length <= 200
// 1 <= nums[i] <= 100
//
//
// Related TopicsArray | Dynamic Programming
//
// 👍 13799, 👎 298bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    // 如何转化为背包问题？
    //      给一个可装载重量为 sum / 2 的背包和 N 个物品，每个物品的重量为 nums[i]。
    //      现在让你装物品，是否存在一种装法，能够恰好将背包装满？

    // 【状态】：「背包的容量」和「可选择的物品」
    // 【选择】：「装进背包」或者「不装进背包」

    // dp数组的定义：
    //      dp[i][j] = x 表示，对于前 i 个物品（i 从 1 开始计数），当前背包的容量为 j 时
    //      若 x 为 true，则说明可以恰好将背包装满
    //      若 x 为 false，则说明不能恰好将背包装满。
    //      根据这个定义，我们想求的最终答案就是 dp[N][sum/2]
    //      base case 就是 dp[..][0] = true 和 dp[0][..] = false
    //      因为背包没有空间的时候，就相当于装满了，而当没有物品可选择的时候，肯定没办法装满背包。

    // 状态转移：
    //      如果不把 nums[i] 算入子集，或者说不把这第 i 个物品装入背包
    //      那么是否能够恰好装满背包，取决于上一个状态 dp[i-1][j]，继承之前的结果
    //      如果把 nums[i] 算入子集，或者说把这第 i 个物品装入了背包，那么是否能够恰好装满背包，取决于状态 dp[i-1][j-nums[i-1]]。

    public bool CanPartition(int[] nums)
    {
        var sum = 0;
        foreach (var num in nums)
        {
            sum += num;
        }

        // 和为奇数时，不可能划分成两个和相等的集合
        if (sum % 2 != 0) return false;

        var n = nums.Length;
        sum = sum / 2;
        var dp = new bool[n + 1][];
        for (int i = 0; i < n + 1; i++)
        {
            dp[i] = new bool[sum + 1];
        }

        // base case
        for (int i = 0; i <= n; i++)
        {
            dp[i][0] = true;
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= sum; j++)
            {
                // 背包容量不足，不能装入第 i 个物品
                if (j - nums[i - 1] < 0)
                {
                    dp[i][j] = dp[i - 1][j];
                }
                else // 装入或不装入背包
                {
                    dp[i][j] = dp[i - 1][j] || dp[i - 1][j - nums[i - 1]];
                }
            }
        }

        return dp[n][sum];
    }

    public bool CanPartitionTableCompress(int[] nums)
    {
        var sum = 0;
        foreach (var num in nums)
        {
            sum += num;
        }

        if (sum % 2 != 0) return false;

        var n = nums.Length;
        sum = sum / 2;
        var dp = new bool[sum + 1];

        // base case
        dp[0] = true;

        for (int i = 0; i < n; i++)
        {
            for (int j = sum; j >= 0; j--) // 从后往前遍历，这样 dp[j-1] 还是上一轮的值，不会被本轮的更新污染。
            {
                if (j - nums[i] >= 0)
                {
                    dp[j] = dp[j] || dp[j - nums[i]];
                }
            }
        }

        return dp[sum];
    }
}
// @lc code=end
