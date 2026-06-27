namespace Scratch.Labuladong.Algorithms.CorporateFlightBookings;

// 1109. Corporate Flight Bookings (Medium)
//
// There are n flights that are labeled from 1 to n.
//
// You are given an array of flight bookings bookings, where bookings[i] = [first_i, last_i,
// seats_i] represents a booking for flights first_i through last_i (inclusive) with seats_i seats
// reserved for each flight in the range.
//
// Return an array answer of length n, where answer[i] is the total number of seats reserved for
// flight i.
//
// Example 1:
//
// Input: bookings = [[1,2,10],[2,3,20],[2,5,25]], n = 5
// Output: [10,55,45,25,25]
// Explanation:
// Flight labels: 1 2 3 4 5
// Booking 1 reserved: 10 10
// Booking 2 reserved: 20 20
// Booking 3 reserved: 25 25 25 25
// Total seats: 10 55 45 25 25
// Hence, answer = [10,55,45,25,25]
//
// Example 2:
//
// Input: bookings = [[1,2,10],[2,2,15]], n = 2
// Output: [10,25]
// Explanation:
// Flight labels: 1 2
// Booking 1 reserved: 10 10
// Booking 2 reserved: 15
// Total seats: 10 25
// Hence, answer = [10,25]
//
// Constraints:
//
// - 1 <= n <= 2 * 10^4
//
// - 1 <= bookings.length <= 2 * 10^4
//
// - bookings[i].length == 3
//
// - 1 <= first_i <= last_i <= n
//
// - 1 <= seats_i <= 10^4
//
// Related Topics: Array, Prefix Sum

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] CorpFlightBookings(int[][] bookings, int n)
    {
        // nums 初始化为全 0
        var nums = new int[n];

        var diff = new Difference(nums);

        foreach (var booking in bookings)
        {
            int i = booking[0] - 1, j = booking[1] - 1;
            var seats = booking[2];

            diff.Increment(i, j, seats);
        }

        return diff.Result();
    }

    class Difference
    {
        // 差分数组
        private int[] Diff;

        public Difference(int[] nums)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(nums.Length);
            Diff = new int[nums.Length];
            // 构造差分数组
            Diff[0] = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                Diff[i] = nums[i] - nums[i - 1];
            }
        }

        // 给闭区间 [i, j] 增加 val（可以是负数）
        public void Increment(int i, int j, int val)
        {
            Diff[i] += val;
            if (j + 1 < Diff.Length)
            {
                Diff[j + 1] -= val;
            }
        }

        public int[] Result()
        {
            int[] res = new int[Diff.Length];
            // 根据差分数组构造结果数组
            res[0] = Diff[0];
            for (int i = 1; i < Diff.Length; i++)
            {
                res[i] = res[i - 1] + Diff[i];
            }

            return res;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
