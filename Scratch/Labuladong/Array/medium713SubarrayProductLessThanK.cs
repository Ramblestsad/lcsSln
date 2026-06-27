namespace Scratch.Labuladong.Algorithms.SubarrayProductLessThanK;

// 713. Subarray Product Less Than K (Medium)
//
// Given an array of integers nums and an integer k, return the number of contiguous subarrays
// where the product of all the elements in the subarray is strictly less than k.
//
// Example 1:
//
// Input: nums = [10,5,2,6], k = 100
// Output: 8
// Explanation: The 8 subarrays that have product less than 100 are:
// [10], [5], [2], [6], [10, 5], [5, 2], [2, 6], [5, 2, 6]
// Note that [10, 5, 2] is not included as the product of 100 is not strictly less than k.
//
// Example 2:
//
// Input: nums = [1,2,3], k = 0
// Output: 0
//
// Constraints:
//
// - 1 <= nums.length <= 3 * 10^4
//
// - 1 <= nums[i] <= 1000
//
// - 0 <= k <= 10^6
//
// Related Topics: Array, Binary Search, Sliding Window, Prefix Sum

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int NumSubarrayProductLessThanK(int[] nums, int k)
    {
        int left = 0, right = 0;
        // 滑动窗口，初始化为乘法单位元
        var windowProduct = 1;
        // 记录符合条件的子数组个数
        var count = 0;

        while (right < nums.Length)
        {
            windowProduct *= nums[right];
            right++;

            while (windowProduct >= k && left < right)
            {
                windowProduct /= nums[left];
                left++;
            }

            // 现在必然是一个合法的窗口，但注意思考这个窗口中的子数组个数怎么计算：
            // 比方说 left = 1, right = 4 划定了 [1, 2, 3] 这个窗口（right 是开区间）
            // 但不止 [left..right] 是合法的子数组，[left+1..right], [left+2..right] 等都是合法子数组
            // 所以需要把 [3], [2,3], [1,2,3] 这 right - left 个子数组都加上
            count += right - left;
        }

        return count;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
