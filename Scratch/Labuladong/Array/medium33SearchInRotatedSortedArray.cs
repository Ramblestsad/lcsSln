/*
 * @lc app=leetcode id=33 lang=csharp
 * @lcpr version=30403
 *
 * [33] Search in Rotated Sorted Array
 */

namespace Scratch.Labuladong.Algorithms.SearchInRotatedSortedArr;

// @lc code=start
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
// @lc code=end

/*
// @lcpr case=start
// [4,5,6,7,0,1,2]\n0\n
// @lcpr case=end

// @lcpr case=start
// [4,5,6,7,0,1,2]\n3\n
// @lcpr case=end

// @lcpr case=start
// [1]\n0\n
// @lcpr case=end
 */
