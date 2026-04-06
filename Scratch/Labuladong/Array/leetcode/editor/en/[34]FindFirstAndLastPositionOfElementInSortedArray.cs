/*
 * @lc app=leetcode id=34 lang=csharp
 * @lcpr version=30402
 *
 * [34] Find First And Last Position Of Element In Sorted Array
 */

namespace Scratch.Labuladong.Algorithms.FindFirstAndLastPositionOfElementInSortedArray;

// @lc code=start
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
// @lc code=end
