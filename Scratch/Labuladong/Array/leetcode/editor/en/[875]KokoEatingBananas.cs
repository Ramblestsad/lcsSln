namespace Scratch.Labuladong.Algorithms.KokoEatingBananas;

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
