namespace Scratch.Labuladong.Algorithms.FirstMissingPositive;

// 41. First Missing Positive (Hard)
//
// Given an unsorted integer array nums. Return the smallest positive integer that is not present
// in nums.
//
// You must implement an algorithm that runs in O(n) time and uses O(1) auxiliary space.
//
// Example 1:
//
// Input: nums = [1,2,0]
// Output: 3
// Explanation: The numbers in the range [1,2] are all in the array.
//
// Example 2:
//
// Input: nums = [3,4,-1,1]
// Output: 2
// Explanation: 1 is in the array but 2 is missing.
//
// Example 3:
//
// Input: nums = [7,8,9,11,12]
// Output: 1
// Explanation: The smallest positive integer 1 is missing.
//
// Constraints:
//
// - 1 <= nums.length <= 10^5
//
// - -2^31 <= nums[i] <= 2^31 - 1
//
// Related Topics: Array, Hash Table

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int FirstMissingPositive(int[] nums)
    {
        var n = nums.Length;
        // 第一步：把无效数字（非正数或超范围）替换成哨兵
        for (var i = 0; i < n; i++)
        {
            if (nums[i] <= 0 || nums[i] > n)
            {
                nums[i] = n + 1;
            }
        }

        // 第二步：用符号位标记 x 出现过，把 nums[x-1] 取反
        for (var i = 0; i < n; i++)
        {
            var x = Math.Abs(nums[i]);
            if (x <= n)
            {
                nums[x - 1] = -Math.Abs(nums[x - 1]);
            }
        }

        // 第三步：第一个正数所在下标 i，说明 i+1 缺失
        for (var i = 0; i < n; i++)
        {
            if (nums[i] > 0)
            {
                return i + 1;
            }
        }

        return n + 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
