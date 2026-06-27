namespace Scratch.Labuladong.Algorithms.SplitArrayLargestSum;

// 410. Split Array Largest Sum (Hard)
//
// Given an integer array nums and an integer k, split nums into k non-empty subarrays such that
// the largest sum of any subarray is minimized.
//
// Return the minimized largest sum of the split.
//
// A subarray is a contiguous part of the array.
//
// Example 1:
//
// Input: nums = [7,2,5,10,8], k = 2
// Output: 18
// Explanation: There are four ways to split nums into two subarrays.
// The best way is to split it into [7,2,5] and [10,8], where the largest sum among the two
// subarrays is only 18.
//
// Example 2:
//
// Input: nums = [1,2,3,4,5], k = 2
// Output: 9
// Explanation: There are four ways to split nums into two subarrays.
// The best way is to split it into [1,2,3] and [4,5], where the largest sum among the two
// subarrays is only 9.
//
// Constraints:
//
// - 1 <= nums.length <= 1000
//
// - 0 <= nums[i] <= 10^6
//
// - 1 <= k <= min(50, nums.length)
//
// Related Topics: Array, Binary Search, Dynamic Programming, Greedy, Prefix Sum

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int SplitArray(int[] nums, int k)
    {
        int left = 0, right = 0;

        foreach (var num in nums)
        {
            left = Math.Max(left, num);
            right += num;
        }

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (F(nums, mid) < k) right = mid - 1;
            else if (F(nums, mid) > k) left = mid + 1;
            else right = mid - 1; // F(x) == k
        }

        return left;
    }

    static int F(int[] nums, int x)
    {
        var days = 0;

        for (int i = 0; i < nums.Length;)
        {
            var cap = x;
            while (i < nums.Length)
            {
                if (cap < nums[i]) break;
                cap -= nums[i];
                i++;
            }

            days++;
        }

        return days;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
