namespace Scratch.Labuladong.Algorithms.CapacityToShipPackagesWithinDDays;

// 1011. Capacity To Ship Packages Within D Days (Medium)
//
// A conveyor belt has packages that must be shipped from one port to another within days days.
//
// The i^th package on the conveyor belt has a weight of weights[i]. Each day, we load the ship
// with packages on the conveyor belt (in the order given by weights). We may not load more weight
// than the maximum weight capacity of the ship.
//
// Return the least weight capacity of the ship that will result in all the packages on the
// conveyor belt being shipped within days days.
//
// Example 1:
//
// Input: weights = [1,2,3,4,5,6,7,8,9,10], days = 5
// Output: 15
// Explanation: A ship capacity of 15 is the minimum to ship all the packages in 5 days like this:
// 1st day: 1, 2, 3, 4, 5
// 2nd day: 6, 7
// 3rd day: 8
// 4th day: 9
// 5th day: 10
//
// Note that the cargo must be shipped in the order given, so using a ship of capacity 14 and
// splitting the packages into parts like (2, 3, 4, 5), (1, 6, 7), (8), (9), (10) is not allowed.
//
// Example 2:
//
// Input: weights = [3,2,2,4,1,4], days = 3
// Output: 6
// Explanation: A ship capacity of 6 is the minimum to ship all the packages in 3 days like this:
// 1st day: 3, 2
// 2nd day: 2, 4
// 3rd day: 1, 4
//
// Example 3:
//
// Input: weights = [1,2,3,1,1], days = 4
// Output: 3
// Explanation:
// 1st day: 1
// 2nd day: 2
// 3rd day: 3
// 4th day: 1, 1
//
// Constraints:
//
// - 1 <= days <= weights.length <= 5 * 10^4
//
// - 1 <= weights[i] <= 500
//
// Related Topics: Array, Binary Search

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
