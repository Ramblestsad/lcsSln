namespace Scratch.Labuladong.Algorithms.KokoEatingBananas;

// 875. Koko Eating Bananas (Medium)
//
// Koko loves to eat bananas. There are n piles of bananas, the i^th pile has piles[i] bananas. The
// guards have gone and will come back in h hours.
//
// Koko can decide her bananas-per-hour eating speed of k. Each hour, she chooses some pile of
// bananas and eats k bananas from that pile. If the pile has less than k bananas, she eats all of
// them instead and will not eat any more bananas during this hour.
//
// Koko likes to eat slowly but still wants to finish eating all the bananas before the guards
// return.
//
// Return the minimum integer k such that she can eat all the bananas within h hours.
//
// Example 1:
//
// Input: piles = [3,6,7,11], h = 8
// Output: 4
//
// Example 2:
//
// Input: piles = [30,11,23,4,20], h = 5
// Output: 30
//
// Example 3:
//
// Input: piles = [30,11,23,4,20], h = 6
// Output: 23
//
// Constraints:
//
// - 1 <= piles.length <= 10^4
//
// - piles.length <= h <= 10^9
//
// - 1 <= piles[i] <= 10^9
//
// Related Topics: Array, Binary Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        // 找到 x 的取值范围作为二分搜索的搜索区间，初始化 left 和 right 变量
        // 最小速度应该是 1，最大速度是 piles 数组中元素的最大值
        int left = 1, right = 1000000000; // 10^9 is the max pile size

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (F(piles, mid) < h) right = mid - 1;
            else if (F(piles, mid) > h) left = mid + 1;
            else right = mid - 1; // F(x) == h 时，继续向左侧搜索更小的 x
        }

        return left;
    }

    static long F(int[] piles, int x)
    {
        /*
         * 若吃香蕉的速度为 x 根/小时，则需要 f(x) 小时吃完所有香蕉。
         */

        long hours = 0;
        foreach (var p in piles)
        {
            hours += p / x;
            if (p % x > 0) hours++;
        }

        return hours;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
