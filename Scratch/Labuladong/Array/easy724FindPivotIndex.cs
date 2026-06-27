namespace Scratch.Labuladong.Algorithms.FindPivotIndex;

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
