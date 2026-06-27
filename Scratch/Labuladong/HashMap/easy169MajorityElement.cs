namespace Scratch.Labuladong.Algorithms.MajorityElement;

// 169. Majority Element (Easy)
//
// Given an array nums of size n, return the majority element.
//
// The majority element is the element that appears more than ⌊n / 2⌋ times. You may assume that
// the majority element always exists in the array.
//
// Example 1:
//
// Input: nums = [3,2,3]
// Output: 3
// Example 2:
//
// Input: nums = [2,2,1,1,1,2,2]
// Output: 2
//
// Constraints:
//
// - n == nums.length
//
// - 1 <= n <= 5 * 10^4
//
// - -10^9 <= nums[i] <= 10^9
//
// - The input is generated such that a majority element will exist in the array.
//
// Follow-up: Could you solve the problem in linear time and in O(1) space?
//
// Related Topics: Array, Hash Table, Divide and Conquer, Sorting, Counting

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MajorityElement(int[] nums)
    {
        var target = 0;
        var cnt = 0;

        foreach (var num in nums)
        {
            if (cnt == 0)
            {
                // 当cnt为0时，假设num就是众数
                target = num;
                cnt = 1;
            }
            else if (num == target)
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }

        return target;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
