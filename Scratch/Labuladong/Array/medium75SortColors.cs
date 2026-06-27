namespace Scratch.Labuladong.Algorithms.SortColors;

// 75. Sort Colors (Medium)
//
// Given an array nums with n objects colored red, white, or blue, sort them in-place so that
// objects of the same color are adjacent, with the colors in the order red, white, and blue.
//
// We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
//
// You must solve this problem without using the library's sort function.
//
// Example 1:
//
// Input: nums = [2,0,2,1,1,0]
// Output: [0,0,1,1,2,2]
//
// Example 2:
//
// Input: nums = [2,0,1]
// Output: [0,1,2]
//
// Constraints:
//
// - n == nums.length
//
// - 1 <= n <= 300
//
// - nums[i] is either 0, 1, or 2.
//
// Follow up: Could you come up with a one-pass algorithm using only constant extra space?
//
// Related Topics: Array, Two Pointers, Sorting

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public void SortColors(int[] nums)
    {
        // 注意区间的开闭，初始化时区间内应该没有元素
        // 所以我们定义 [0，p0) 是元素 0 的区间，(p2, nums.length - 1] 是 2 的区间
        int p0 = 0, p2 = nums.Length - 1, p = 0;
        // 由于 p2 是开区间，所以 p <= p2
        while (p <= p2)
        {
            if (nums[p] == 0)
            {
                ( nums[p0], nums[p] ) = ( nums[p], nums[p0] );
                p0++;
            }
            else if (nums[p] == 2)
            {
                ( nums[p2], nums[p] ) = ( nums[p], nums[p2] );
                p2--;
            }
            else if (nums[p] == 1)
            {
                p++;
            }

            // 因为小于 p0 都是 0，所以 p 不要小于 p0
            if (p < p0)
            {
                p = p0;
            }
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
