/*
 * @lc app=leetcode id=724 lang=csharp
 * @lcpr version=30402
 *
 * [724] Find Pivot Index
 */

namespace Scratch.Labuladong.Algorithms.FindPivotIndex;

// @lc code=start
public class Solution
{
    public int PivotIndex(int[] nums)
    {
        var n = nums.Length;
        var preSum = new int[n + 1];
        preSum[0] = 0;
        // 计算 nums 的前缀和
        for (int i = 1; i <= n; i++)
        {
            preSum[i] = preSum[i - 1] + nums[i - 1];
        }

        // 根据前缀和判断左半边数组和右半边数组的元素和是否相同
        for (int i = 1; i < preSum.Length; i++)
        {
            // 计算 nums[i-1] 左侧和右侧的元素和
            var leftSum = preSum[i - 1] - preSum[0];
            var rightSum = preSum[n] - preSum[i];
            if (leftSum == rightSum)
            {
                // 相对 nums 数组，preSum 数组有一位索引偏移
                return i - 1;
            }
        }

        return -1;
    }
}
// @lc code=end
/*
@lcpr case=start
[1,7,3,6,5,6]\n
@lcpr case=end

@lcpr case=start
[1,2,3]\n
@lcpr case=end

@lcpr case=start
[2,1,-1]\n
@lcpr case=end
 */
