namespace Scratch.Labuladong.Algorithms.CapacityToShipPackagesWithinDDays;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int ShipWithinDays(int[] weights, int days)
    {
        // target - days
        // x - capacity
        // f(x) - days needed with capacity x
        // f(x) is monotonically decreasing

        int left = 0, right = 0;
        // left, right 的边界分别为 max(weights), sum(weights)
        foreach (var weight in weights)
        {
            left = Math.Max(left, weight);
            right += weight;
        }

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (F(weights, mid) < days) right = mid - 1;
            else if (F(weights, mid) > days) left = mid + 1;
            else right = mid - 1; // F(x) == days, 继续向左找更小的 capacity
        }

        return left;
    }

    static int F(int[] weights, int x)
    {
        // 定义：当运载能力为 x 时，需要 f(x) 天运完所有货物
        // f(x) 随着 x 的增加单调递减

        var days = 0;
        for (int i = 0; i < weights.Length;)
        {
            // 每天capacity为x
            var cap = x;
            // 装货
            while (i < weights.Length)
            {
                // 不能装下当前货物
                if (cap < weights[i]) break;
                // 装得下当前货物
                cap -= weights[i];
                // i 的增长完全由装货逻辑驱动：装得下就前进，
                // 装不下就留在当前 i，外层下一天继续尝试装同一个包裹。
                i++;
            }

            days++;
        }

        return days;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
