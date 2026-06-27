namespace Scratch.Labuladong.Algorithms.MoveZeroes;

// 283. Move Zeroes (Easy)
//
// Given an integer array nums, move all 0's to the end of it while maintaining the relative order
// of the non-zero elements.
//
// Note that you must do this in-place without making a copy of the array.
//
// Example 1:
//
// Input: nums = [0,1,0,3,12]
// Output: [1,3,12,0,0]
// Example 2:
//
// Input: nums = [0]
// Output: [0]
//
// Constraints:
//
// - 1 <= nums.length <= 10^4
//
// - -2^31 <= nums[i] <= 2^31 - 1
//
// Follow up: Could you minimize the total number of operations done?
//
// Related Topics: Array, Two Pointers

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
