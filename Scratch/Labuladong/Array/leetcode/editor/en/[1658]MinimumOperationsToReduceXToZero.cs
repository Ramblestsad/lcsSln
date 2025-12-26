namespace Scratch.Labuladong.Algorithms.MinimumOperationsToReduceXToZero;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 等价于寻找 nums 中元素和为 sum(nums) - x 的最长子数组
    public int MinOperations(int[] nums, int x)
    {
        int n = nums.Length;
        // 窗口内子数组的目标和
        var target = nums.Sum() - x;
        if (target < 0) return -1;
        if (target == 0) return n;

        int left = 0, right = 0, windowSum = 0;
        // windowSum 表示当前窗口内子数组的和
        // maxLen 记录目标子数组的最大长度
        int maxLen = int.MinValue;

        // 滑动窗口框架
        while (right < n)
        {
            // 扩展窗口
            windowSum += nums[right];
            right++;

            while (windowSum > target && left < right)
            {
                // 收缩窗口
                windowSum -= nums[left];
                left++;
            }

            // 寻找目标子数组
            if (windowSum == target) maxLen = Math.Max(maxLen, right - left);
        }

        // 目标子数组的最大长度可以推导出需要的计算步骤
        return maxLen == int.MinValue ? -1 : n - maxLen;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
