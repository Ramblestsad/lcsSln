/*
 * @lc app=leetcode id=319 lang=csharp
 * @lcpr version=30402
 *
 * [319] Bulb Switcher
 */

namespace Scratch.Labuladong.Algorithms.BulbSwitcher;

//There are n bulbs that are initially off. You first turn on all the bulbs,
//then you turn off every second bulb.
//
// On the third round, you toggle every third bulb (turning on if it's off or
//turning off if it's on). For the iᵗʰ round, you toggle every i bulb. For the nᵗʰ
//round, you only toggle the last bulb.
//
// Return the number of bulbs that are on after n rounds.
//
//
// Example 1:
//
//
//Input: n = 3
//Output: 1
//Explanation: At first, the three bulbs are [off, off, off].
//After the first round, the three bulbs are [on, on, on].
//After the second round, the three bulbs are [on, off, on].
//After the third round, the three bulbs are [on, off, off].
//So you should return 1 because there is only one bulb is on.
//
// Example 2:
//
//
//Input: n = 0
//Output: 0
//
//
// Example 3:
//
//
//Input: n = 1
//Output: 1
//
//
//
// Constraints:
//
//
// 0 <= n <= 10⁹
//
//
// Related TopicsMath | Brainteaser
//
// 👍 2951, 👎 3232bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int BulbSwitch(int n)
    {
        return (int)Math.Sqrt(n);
    }
}
// @lc code=end
