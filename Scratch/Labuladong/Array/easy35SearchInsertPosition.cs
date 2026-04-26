/*
 * @lc app=leetcode id=35 lang=csharp
 * @lcpr version=30403
 *
 * [35] Search Insert Position
 */

namespace Scratch.Labuladong.Algorithms.SeatchInsertPos;

// @lc code=start
public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        var i = 0;
        var j = nums.Length;

        while (i < j)
        {
            var mid = i + ( j - i ) / 2;
            if (nums[mid] < target)
            {
                i = mid + 1;
            }
            else if (nums[mid] > target)
            {
                j = mid;
            }
            else
            {
                j = mid;
            }
        }

        return i;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [1,3,5,6]\n5\n
// @lcpr case=end

// @lcpr case=start
// [1,3,5,6]\n2\n
// @lcpr case=end

// @lcpr case=start
// [1,3,5,6]\n7\n
// @lcpr case=end
 */
