/*
 * @lc app=leetcode id=238 lang=csharp
 * @lcpr version=30402
 *
 * [238] Product of Array Except Self
 */

namespace Scratch.Labuladong.Algorithms.ProductOfArrayExceptSelf;

// @lc code=start
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
// @lc code=end
/*
@lcpr case=start
[1,2,3,4]\n
@lcpr case=end

@lcpr case=start
[-1,1,0,-3,3]\n
@lcpr case=end
 */
