namespace Scratch.Labuladong.Algorithms.MinimumOperationsToReduceXToZero;

// 1658. Minimum Operations to Reduce X to Zero (Medium)
//
// You are given an integer array nums and an integer x. In one operation, you can either remove
// the leftmost or the rightmost element from the array nums and subtract its value from x. Note
// that this modifies the array for future operations.
//
// Return the minimum number of operations to reduce x to exactly 0 if it is possible, otherwise,
// return -1.
//
// Example 1:
//
// Input: nums = [1,1,4,2,3], x = 5
// Output: 2
// Explanation: The optimal solution is to remove the last two elements to reduce x to zero.
//
// Example 2:
//
// Input: nums = [5,6,7,8,9], x = 4
// Output: -1
//
// Example 3:
//
// Input: nums = [3,2,20,1,1,3], x = 10
// Output: 5
// Explanation: The optimal solution is to remove the last three elements and the first two
// elements (5 operations in total) to reduce x to zero.
//
// Constraints:
//
// - 1 <= nums.length <= 10^5
//
// - 1 <= nums[i] <= 10^4
//
// - 1 <= x <= 10^9
//
// Related Topics: Array, Hash Table, Binary Search, Sliding Window, Prefix Sum

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
