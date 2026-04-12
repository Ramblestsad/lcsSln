/*
 * @lc app=leetcode id=560 lang=csharp
 * @lcpr version=30402
 *
 * [560] Subarray Sum Equals K
 */

namespace Scratch.Labuladong.Algorithms.SubarrySumEqualK;

// @lc code=start
public class Solution
{
    public int SubarraySum(int[] nums, int k)
    {
        var n = nums.Length;
        var res = 0;
        // presum arr
        var preSum = new int[n + 1];
        preSum[0] = 0;
        // 前缀和到该前缀和出现次数的mapping，方便查找所需的前缀和
        var count = new Dictionary<int, int> { { 0, 1 } };

        // 计算nums前缀和
        for (int i = 1; i <= n; i++)
        {
            preSum[i] = preSum[i - 1] + nums[i - 1];
            // 如果之前存在值为 need 的前缀和
            // 属性存在以 nums[i - 1] 结尾的subarr的和为k
            var need = preSum[i] - k;
            if (count.TryGetValue(need, out var c))
                res += c;
            // 将当前前缀和存入hashmap
            if (!count.TryGetValue(preSum[i], out var pc))
            {
                count.Add(preSum[i], 1);
            }
            else
            {
                count[preSum[i]] = pc + 1;
            }
        }

        return res;
    }
}
// @lc code=end
/*
@lcpr case=start
[1,1,1]\n2\n
@lcpr case=end

@lcpr case=start
[1,2,3]\n3\n
@lcpr case=end
 */
