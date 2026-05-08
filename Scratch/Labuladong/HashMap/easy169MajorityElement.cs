/*
 * @lc app=leetcode id=169 lang=csharp
 * @lcpr version=30403
 *
 * [169] Majority Element
 */

namespace Scratch.Labuladong.Algorithms.MajorityElement;

// @lc code=start
public class Solution
{
    public int MajorityElement(int[] nums)
    {
        var target = 0;
        var cnt = 0;

        foreach (var num in nums)
        {
            if (cnt == 0)
            {
                // 当cnt为0时，假设num就是众数
                target = num;
                cnt = 1;
            }
            else if (num == target)
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }

        return target;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [3,2,3]\n
// @lcpr case=end

// @lcpr case=start
// [2,2,1,1,1,2,2]\n
// @lcpr case=end
 */
