/*
 * @lc app=leetcode id=153 lang=csharp
 * @lcpr version=30403
 *
 * [153] Find Minimum in Rotated Sorted Array
 */

namespace Scratch.Labuladong.Algorithms.FindMinRotatedSortedArr;

// @lc code=start
public class Solution
{
    public int FindMin(int[] nums)
    {
        var left = 0;
        var right = nums.Length - 1;

        while (left < right)
        {
            var mid = left + ( right - left ) / 2;

            if (nums[mid] > nums[right])
            {
                // mid 落在断崖左边，最小值在 [mid+1, right]
                left = mid + 1;
            }
            else
            {
                // mid 落在断崖右边（或本身就是最小值），最小值在 [left, mid]
                right = mid;
            }
        }

        return nums[left];
    }
}
// @lc code=end

/*
@lcpr case=start
[3,4,5,1,2]\n
@lcpr case=end

@lcpr case=start
[4,5,6,7,0,1,2]\n
@lcpr case=end

@lcpr case=start
[11,13,15,17]\n
@lcpr case=end
 */
