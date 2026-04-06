/*
 * @lc app=leetcode id=26 lang=csharp
 * @lcpr version=30402
 *
 * [26] Remove Duplicates From Sorted Array
 */

namespace Scratch.Labuladong.Algorithms.RemoveDuplicatesFromSortedArray;

// @lc code=start
public class Solution
{
    public int RemoveDuplicates(int[] nums)
    {
        if (nums.Length <= 0) return 0;

        /*
         * 慢指针 slow 走在后面，快指针 fast 走在前面探路，找到一个不重复的元素就赋值给 slow + 1
         * 这样，就保证了 nums[0..slow] 都是无重复的元素,
         * 当 fast 指针遍历完整个数组 nums 后，nums[0..slow] 就是整个数组去重之后的结果.
         */

        var slow = 0;
        var fast = 1;

        while (fast < nums.Length)
        {
            if (nums[slow] != nums[fast])
            {
                slow++;
                nums[slow] = nums[fast];
            }

            fast++;
        }

        return slow + 1;
    }
}
// @lc code=end
