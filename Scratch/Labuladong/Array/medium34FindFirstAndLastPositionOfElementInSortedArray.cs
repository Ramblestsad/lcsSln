namespace Scratch.Labuladong.Algorithms.FindFirstAndLastPositionOfElementInSortedArray;

// 34. Find First and Last Position of Element in Sorted Array (Medium)
//
// Given an array of integers nums sorted in non-decreasing order, find the starting and ending
// position of a given target value.
//
// If target is not found in the array, return [-1, -1].
//
// You must write an algorithm with O(log n) runtime complexity.
//
// Example 1:
//
// Input: nums = [5,7,7,8,8,10], target = 8
// Output: [3,4]
// Example 2:
//
// Input: nums = [5,7,7,8,8,10], target = 6
// Output: [-1,-1]
// Example 3:
//
// Input: nums = [], target = 0
// Output: [-1,-1]
//
// Constraints:
//
// - 0 <= nums.length <= 10^5
//
// - -10^9 <= nums[i] <= 10^9
//
// - nums is a non-decreasing array.
//
// - -10^9 <= target <= 10^9
//
// Related Topics: Array, Binary Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] SearchRange(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else if (nums[mid] > target)
            {
                right = mid - 1;
            }
            else // nums[mid] == target
            {
                right = mid - 1;
            }
        }

        // now left is the first position of target
        if (left >= nums.Length || nums[left] != target)
        {
            return [-1, -1];
        }

        var firstPos = left;

        left = 0;
        right = nums.Length - 1;

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else if (nums[mid] > target)
            {
                right = mid - 1;
            }
            else // nums[mid] == target
            {
                left = mid + 1;
            }
        }

        // now right is the last position of target
        var lastPos = right;
        return [firstPos, lastPos];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
