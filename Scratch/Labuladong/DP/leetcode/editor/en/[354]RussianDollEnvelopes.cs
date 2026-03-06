namespace Scratch.Labuladong.Algorithms.RussianDollEnvelopes;

//You are given a 2D array of integers envelopes where envelopes[i] = [wi, hi]
//represents the width and the height of an envelope.
//
// One envelope can fit into another if and only if both the width and height
//of one envelope are greater than the other envelope's width and height.
//
// Return the maximum number of envelopes you can Russian doll (i.e., put one
//inside the other).
//
// Note: You cannot rotate an envelope.
//
//
// Example 1:
//
//
//Input: envelopes = [[5,4],[6,4],[6,7],[2,3]]
//Output: 3
//Explanation: The maximum number of envelopes you can Russian doll is 3 ([2,3]
//=> [5,4] => [6,7]).
//
//
// Example 2:
//
//
//Input: envelopes = [[1,1],[1,1],[1,1]]
//Output: 1
//
//
//
// Constraints:
//
//
// 1 <= envelopes.length <= 10⁵
// envelopes[i].length == 2
// 1 <= wi, hi <= 10⁵
//
//
// Related TopicsArray | Binary Search | Dynamic Programming | Sorting
//
// 👍 6452, 👎 170bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 这道题目其实是最长递增子序列的一个变种。
    // 因为每次合法的嵌套是大的套小的，相当于在二维平面中找一个最长递增的子序列，其长度就是最多能嵌套的信封个数。
    // 先对宽度 w 进行升序排序，如果遇到 w 相同的情况，则按照高度 h 降序排序；
    // 之后把所有的 h 作为一个数组，在这个数组上计算 LIS 的长度就是答案。
    // https://labuladong.online/images/algo/nest-envelope/1.jpg
    public int MaxEnvelopes(int[][] envelopes)
    {
        var n = envelopes.Length;
        // 按宽度升序排列，如果宽度一样，则按高度降序排列
        Array.Sort(envelopes, (a, b) =>
        {
            if (a[0] == b[0])
                return b[1].CompareTo(a[1]);
            return a[0].CompareTo(b[0]);
        });

        // 对高度数组寻找 LIS
        var heights = new int[n];
        for (int i = 0; i < n; i++)
        {
            heights[i] = envelopes[i][1];
        }

        return _lengthOfLIS(heights);
    }

    // 返回 nums 中 LIS 的长度
    private int _lengthOfLIS(int[] nums)
    {
        var piles = 0;
        var n = nums.Length;
        var top = new int[n];

        for (int i = 0; i < n; i++)
        {
            // 要处理的扑克牌
            var poker = nums[i];
            var left = 0;
            var right = piles;
            // 二分查找插入位置
            while (left < right)
            {
                var mid = ( left + right ) / 2;
                if (top[mid] >= poker)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }

            if (left == piles) piles++;
            // 把这张牌放到牌堆顶
            top[left] = poker;
        }

        return piles;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
