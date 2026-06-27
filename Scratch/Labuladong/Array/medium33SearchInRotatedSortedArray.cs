namespace Scratch.Labuladong.Algorithms.SearchInRotatedSortedArr;

// 33. Search in Rotated Sorted Array (Medium)
//
// There is an integer array nums sorted in ascending order (with distinct values).
//
// Prior to being passed to your function, nums is possibly left rotated at an unknown index k (1
// <= k < nums.length) such that the resulting array is [nums[k], nums[k+1], ..., nums[n-1],
// nums[0], nums[1], ..., nums[k-1]] (0-indexed). For example, [0,1,2,4,5,6,7] might be left
// rotated by 3 indices and become [4,5,6,7,0,1,2].
//
// Given the array nums after the possible rotation and an integer target, return the index of
// target if it is in nums, or -1 if it is not in nums.
//
// You must write an algorithm with O(log n) runtime complexity.
//
// Example 1:
//
// Input: nums = [4,5,6,7,0,1,2], target = 0
// Output: 4
// Example 2:
//
// Input: nums = [4,5,6,7,0,1,2], target = 3
// Output: -1
// Example 3:
//
// Input: nums = [1], target = 0
// Output: -1
//
// Constraints:
//
// - 1 <= nums.length <= 5000
//
// - -10^4 <= nums[i] <= 10^4
//
// - All values of nums are unique.
//
// - nums is an ascending array that is possibly rotated.
//
// - -10^4 <= target <= 10^4
//
// Related Topics: Array, Binary Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int Search(int[] nums, int target)
    {
        var left = 0;
        var right = nums.Length;

        while (left < right)
        {
            var mid = left + ( right - left ) / 2;
            if (nums[mid] == target) return mid;
            if (nums[mid] >= nums[left])
            {
                // mid 落在断崖左边, nums[left..mid]有序
                if (target >= nums[left] && target < nums[mid])
                {
                    // target 落在 [left..mid)中
                    right = mid;
                }
                else
                {
                    // target 落在 [mid + 1..right]中
                    left = mid + 1;
                }
            }
            else
            {
                // mid 落在断崖右边, nums[mid..right]有序
                if (target <= nums[right - 1] && target > nums[mid])
                {
                    // target 落在 [mid + 1..right]中
                    left = mid + 1;
                }
                else
                {
                    // target 落在 [left..mid)中
                    right = mid;
                }
            }
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
