namespace Scratch.Labuladong.Algorithms.ShortestUnsortedContinuousSubarray;

// 581. Shortest Unsorted Continuous Subarray (Medium)
//
// Given an integer array nums, you need to find one continuous subarray such that if you only sort
// this subarray in non-decreasing order, then the whole array will be sorted in non-decreasing
// order.
//
// Return the shortest such subarray and output its length.
//
// Example 1:
//
// Input: nums = [2,6,4,8,10,9,15]
// Output: 5
// Explanation: You need to sort [6, 4, 8, 10, 9] in ascending order to make the whole array sorted
// in ascending order.
//
// Example 2:
//
// Input: nums = [1,2,3,4]
// Output: 0
//
// Example 3:
//
// Input: nums = [1]
// Output: 0
//
// Constraints:
//
// - 1 <= nums.length <= 10^4
//
// - -10^5 <= nums[i] <= 10^5
//
// Follow up: Can you solve it in O(n) time complexity?
//
// Related Topics: Array, Two Pointers, Stack, Greedy, Sorting, Monotonic Stack

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int FindUnsortedSubarray(int[] nums)
    {
        // 排序解法
        var sortedNums = new int[nums.Length];
        Array.Copy(nums, sortedNums, nums.Length);
        Array.Sort(sortedNums);

        var left = int.MaxValue;
        var right = int.MinValue;

        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] != sortedNums[i])
            {
                left = i;
                break;
            }
        }

        for (var i = nums.Length - 1; i >= 0; i--)
        {
            if (nums[i] != sortedNums[i])
            {
                right = i;
                break;
            }
        }

        if (left == int.MaxValue && right == int.MinValue) return 0; // 本来有序

        return right - left + 1;
    }

    public int FindUnsortedSubarrayMonotonicStk(int[] nums)
    {
        var n = nums.Length;
        int left = int.MaxValue, right = int.MinValue;

        // 递增栈，存储元素索引
        var incrStk = new Stack<int>();
        for (var i = 0; i < n; i++)
        {
            while (incrStk.Count != 0 && nums[i] < nums[incrStk.Peek()])
            {
                left = Math.Min(left, incrStk.Pop());
            }

            incrStk.Push(i);
        }

        // 递减栈，存储元素索引
        var decrStk = new Stack<int>();
        for (var i = n - 1; i >= 0; i--)
        {
            while (decrStk.Count != 0 && nums[i] > nums[decrStk.Peek()])
            {
                right = Math.Max(right, decrStk.Pop());
            }

            decrStk.Push(i);
        }

        if (left == int.MaxValue && right == int.MinValue) return 0; // 本来有序

        return right - left + 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
