/*
 * @lc app=leetcode id=283 lang=csharp
 * @lcpr version=30402
 *
 * [283] Move Zeroes
 */

namespace Scratch.Labuladong.Algorithms.MoveZeroes;

// @lc code=start
public class Solution
{
    public void MoveZeroes(int[] nums)
    {
        // 变形为先拿到去除0的数组，然后加0
        var p = RemoveElement(nums, 0);

        // for (; p < nums.Length; p++)
        // {
        //     nums[p] = 0;
        // }
        // more efficient way
        nums.AsSpan(p).Clear();
    }

    private static int RemoveElement(int[] nums, int val)
    {
        var slow = 0;
        var fast = 0;

        while (fast < nums.Length)
        {
            if (nums[fast] != val)
            {
                // 保留不同的元素，slow移动到下一个位置
                nums[slow] = nums[fast];
                slow++;
            }

            // fast 遍历nums
            fast++;
        }

        return slow;
    }
}
// @lc code=end
