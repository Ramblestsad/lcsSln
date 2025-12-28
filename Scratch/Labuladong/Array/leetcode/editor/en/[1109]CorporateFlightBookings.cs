namespace Scratch.Labuladong.Algorithms.CorporateFlightBookings;

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
