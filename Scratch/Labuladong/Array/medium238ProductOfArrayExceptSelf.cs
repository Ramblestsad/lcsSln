namespace Scratch.Labuladong.Algorithms.ProductOfArrayExceptSelf;

// 238. Product of Array Except Self (Medium)
//
// Given an integer array nums, return an array answer such that answer[i] is equal to the product
// of all the elements of nums except nums[i].
//
// The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.
//
// You must write an algorithm that runs in O(n) time and without using the division operation.
//
// Example 1:
//
// Input: nums = [1,2,3,4]
// Output: [24,12,8,6]
// Example 2:
//
// Input: nums = [-1,1,0,-3,3]
// Output: [0,0,9,0,0]
//
// Constraints:
//
// - 2 <= nums.length <= 10^5
//
// - -30 <= nums[i] <= 30
//
// - The input is generated such that answer[i] is guaranteed to fit in a 32-bit integer.
//
// Follow up: Can you solve the problem in O(1) extra space complexity? (The output array does not
// count as extra space for space complexity analysis.)
//
// Related Topics: Array, Prefix Sum

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] ProductExceptSelf(int[] nums)
    {
        var n = nums.Length;
        // 从左到右的前缀积；prefix[i] 是 nums[0..i]的积
        var prefix = new int[n];
        prefix[0] = nums[0];
        for (int i = 1; i < n; i++)
        {
            prefix[i] = prefix[i - 1] * nums[i];
        }

        // 从右到左的后缀积；suffix[i] 是 nums[i..n-1]的积
        var suffix = new int[n];
        suffix[n - 1] = nums[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            suffix[i] = suffix[i + 1] * nums[i];
        }

        var answer = new int[n];
        answer[0] = suffix[1];
        answer[n - 1] = prefix[n - 2];
        for (int i = 1; i < n - 1; i++)
        {
            // 除了nums[i] 自己之外的其他元素积就是 nums[i] 左侧和右侧的所有元素之积
            answer[i] = prefix[i - 1] * suffix[i + 1];
        }

        return answer;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
