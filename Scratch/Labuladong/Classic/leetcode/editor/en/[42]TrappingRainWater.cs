/*
 * @lc app=leetcode id=42 lang=csharp
 * @lcpr version=30402
 *
 * [42] Trapping Rain Water
 */

namespace Scratch.Labuladong.Algorithms.TrappingRainWater;

//Given n non-negative integers representing an elevation map where the width
//of each bar is 1, compute how much water it can trap after raining.
//
//
// Example 1:
//
//
//Input: height = [0,1,0,2,1,0,1,3,2,1,2,1]
//Output: 6
//Explanation: The above elevation map (black section) is represented by array [
//0,1,0,2,1,0,1,3,2,1,2,1]. In this case, 6 units of rain water (blue section)
//are being trapped.
//
//
// Example 2:
//
//
//Input: height = [4,2,0,3,2,5]
//Output: 9
//
//
//
// Constraints:
//
//
// n == height.length
// 1 <= n <= 2 * 10⁴
// 0 <= height[i] <= 10⁵
//
//
// Related TopicsArray | Two Pointers | Dynamic Programming | Stack | Monotonic
//Stack
//
// 👍 36208, 👎 692bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int Trap(int[] height)
    {
        return TrapTwoP(height);
    }

    public int TrapNaive(int[] height)
    {
        var n = height.Length;
        var res = 0;

        // 最左、最右肯定是无法留住雨水的
        // 所以遍历 1 ~ n - 1
        for (int i = 1; i < n - 1; i++)
        {
            var leftMax = 0;
            var rightMax = 0;

            for (int j = i; j < n; j++)
            {
                rightMax = Math.Max(rightMax, height[j]);
            }

            for (int j = i; j >= 0; j--)
            {
                leftMax = Math.Max(leftMax, height[j]);
            }

            // 如果自己就是最高的话，
            // l_max == r_max == height[i]
            res += Math.Min(leftMax, rightMax) - height[i];
        }

        return res;
    }

    public int TrapMemo(int[] height)
    {
        if (height.Length == 0) return 0;
        var n = height.Length;
        var res = 0;

        // 数组充当备忘录
        var lMax = new int[n];
        var rMax = new int[n];
        // 初始化 base case
        lMax[0] = height[0];
        rMax[n - 1] = height[n - 1];

        // 从左向右计算 l_max
        for (int i = 1; i < n; i++)
            lMax[i] = Math.Max(height[i], lMax[i - 1]);

        // 从右向左计算 r_max
        for (int i = n - 2; i >= 0; i--)
            rMax[i] = Math.Max(height[i], rMax[i + 1]);

        // 计算答案
        for (int i = 1; i < n - 1; i++)
            res += Math.Min(lMax[i], rMax[i]) - height[i];

        return res;
    }

    public int TrapTwoP(int[] height)
    {
        var left = 0;
        var right = height.Length - 1;
        var lMax = 0;
        var rMax = 0;

        var res = 0;

        while (left < right)
        {
            lMax = Math.Max(lMax, height[left]);
            rMax = Math.Max(rMax, height[right]);

            if (lMax < rMax)
            {
                // 此时height[left] 并不关心rMax到底是不是右边最大的
                res += lMax - height[left];
                left++;
            }
            else
            {
                // 此时height[right] 也并不关心lMax到底是不是左边最大的
                res += rMax - height[right];
                right--;
            }
            // 不需要担心另一边接不住会漏，因为另一边肯定更大......
        }

        return res;
    }
}
// @lc code=end
