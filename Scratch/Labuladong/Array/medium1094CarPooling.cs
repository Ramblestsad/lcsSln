namespace Scratch.Labuladong.Algorithms.CarPooling;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool CarPooling(int[][] trips, int capacity)
    {
        // 根据题目，车站号0 ～ 1000
        var nums = new int[1001];
        var diff = new Difference(nums);

        foreach (var trip in trips)
        {
            var numPassengers = trip[0];
            // 第 trip[1] 站乘客上车
            var i = trip[1];
            // 第 trip[2] 站乘客已经下车，
            // 即乘客在车上的区间是 [trip[1], trip[2] - 1]
            var j = trip[2] - 1;
            diff.Increment(i, j, numPassengers);
        }

        var res = diff.Result();

        for (int i = 0; i < res.Length; i++)
        {
            if (capacity < res[i]) return false;
        }

        return true;
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
