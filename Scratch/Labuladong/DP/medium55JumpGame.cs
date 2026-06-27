namespace Scratch.Labuladong.Algorithms.JumpGame;

//You are given an integer array nums. You are initially positioned at the
//array's first index, and each element in the array represents your maximum jump
//length at that position.
//
// Return true if you can reach the last index, or false otherwise.
//
//
// Example 1:
//
//
//Input: nums = [2,3,1,1,4]
//Output: true
//Explanation: Jump 1 step from index 0 to 1, then 3 steps to the last index.
//
//
// Example 2:
//
//
//Input: nums = [3,2,1,0,4]
//Output: false
//Explanation: You will always arrive at index 3 no matter what. Its maximum
//jump length is 0, which makes it impossible to reach the last index.
//
//
//
// Constraints:
//
//
// 1 <= nums.length <= 10⁴
// 0 <= nums[i] <= 10⁵
//
//
// Related TopicsArray | Dynamic Programming | Greedy
//
// 👍 21650, 👎 1487bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool CanJump(int[] nums)
    {
        var n = nums.Length;
        var farthest = 0;

        for (int i = 0; i < n - 1; i++)
        {
            // 不断计算能跳到的最远距离
            farthest = Math.Max(farthest, i + nums[i]);
            // 可能碰到了 0，卡住跳不动了
            if (farthest <= i) return false;
        }

        return farthest >= n - 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
