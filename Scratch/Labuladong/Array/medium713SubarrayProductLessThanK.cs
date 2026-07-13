/*
 * @lc app=leetcode id=713 lang=csharp
 * @lcpr version=3.4.4
 */

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

// @lc code=start
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
// @lc code=end

/*
// @lcpr case=start
// [686, 28, 455, 675, 605, 29, 942, 48, 502, 889, 854, 206, 231, 796, 272, 565, 887, 969, 558, 13, 22, 455, 145, 804, 15]\n515854\n
// @lcpr case=end

// @lcpr case=start
// [542, 433, 935, 193, 280, 849, 122, 107, 688, 913, 31, 311, 814, 507, 596, 109, 340, 981, 662, 145, 955, 692, 659, 46, 276, 734, 177, 727, 329, 320, 93, 78, 451, 129, 226, 491, 595, 175, 894, 662, 699, 871, 340, 375, 98, 38, 414, 306, 20, 548, 459, 577, 626, 942, 92, 322, 665, 497, 593, 877, 247, 487, 67, 320, 78, 775, 431, 193, 175, 957, 926, 816, 776, 967, 600, 114, 474, 810, 513, 43, 586, 559, 880, 540, 122, 95, 408, 621, 850, 598]\n425740\n
// @lcpr case=end
 */
