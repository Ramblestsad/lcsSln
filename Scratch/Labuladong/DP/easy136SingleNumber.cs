/*
 * @lc app=leetcode id=136 lang=csharp
 * @lcpr version=3.4.4
 */

namespace Scratch.Labuladong.Algorithms.SingleNumber;

// 136. Single Number (Easy)
//
// Given a non-empty array of integers nums, every element appears twice except for one. Find that
// single one.
//
// You must implement a solution with a linear runtime complexity and use only constant extra
// space.
//
// Example 1:
//
// Input: nums = [2,2,1]
//
// Output: 1
//
// Example 2:
//
// Input: nums = [4,1,2,1,2]
//
// Output: 4
//
// Example 3:
//
// Input: nums = [1]
//
// Output: 1
//
// Constraints:
//
// - 1 <= nums.length <= 3 * 10^4
//
// - -3 * 10^4 <= nums[i] <= 3 * 10^4
//
// - Each element in the array appears twice except for one element which appears only once.
//
// Related Topics: Array, Bit Manipulation

// @lc code=start
public class Solution
{
    public int SingleNumber(int[] nums)
    {
        var res = 0;

        foreach (var num in nums)
        {
            res ^= num;
        }

        return res;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [2,2,1]\n
// @lcpr case=end

// @lcpr case=start
// [4,1,2,1,2]\n
// @lcpr case=end

// @lcpr case=start
// [1]\n
// @lcpr case=end
 */
