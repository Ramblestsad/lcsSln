namespace Scratch.Labuladong.Algorithms.ContainerWithMostWater;

//You are given an integer array height of length n. There are n vertical lines
//drawn such that the two endpoints of the iᵗʰ line are (i, 0) and (i, height[i]).
//
//
// Find two lines that together with the x-axis form a container, such that the
//container contains the most water.
//
// Return the maximum amount of water a container can store.
//
// Notice that you may not slant the container.
//
//
// Example 1:
//
//
//Input: height = [1,8,6,2,5,4,8,3,7]
//Output: 49
//Explanation: The above vertical lines are represented by array [1,8,6,2,5,4,8,
//3,7]. In this case, the max area of water (blue section) the container can
//contain is 49.
//
//
// Example 2:
//
//
//Input: height = [1,1]
//Output: 1
//
//
//
// Constraints:
//
//
// n == height.length
// 2 <= n <= 10⁵
// 0 <= height[i] <= 10⁴
//
//
// Related TopicsArray | Two Pointers | Greedy
//
// 👍 34002, 👎 2191bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MaxArea(int[] height)
    {
        var left = 0;
        var right = height.Length - 1;
        var res = 0;

        while (left < right)
        {
            // [left, right] 之间的矩形面积
            var curArea = Math.Min(height[left], height[right]) * ( right - left );
            res = Math.Max(res, curArea);

            // 双指针技巧，移动较低的一边
            if (height[left] < height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
