/*
 * @lc app=leetcode id=27 lang=csharp
 * @lcpr version=30402
 *
 * [27] Remove Element
 */

namespace Scratch.Labuladong.Algorithms.RemoveElement;

// @lc code=start
public class Solution
{
    public int RemoveElement(int[] nums, int val)
    {
        var fast = 0;
        var slow = 0;

        while (fast < nums.Length)
        {
            if (nums[fast] != val)
            {
                nums[slow] = nums[fast];
                slow++;
            }

            fast++;
        }

        return slow;
    }
}
// @lc code=end
