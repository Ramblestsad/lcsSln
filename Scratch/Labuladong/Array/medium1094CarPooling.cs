namespace Scratch.Labuladong.Algorithms.CarPooling;

// 1094. Car Pooling (Medium)
//
// There is a car with capacity empty seats. The vehicle only drives east (i.e., it cannot turn
// around and drive west).
//
// You are given the integer capacity and an array trips where trips[i] = [numPassengers_i, from_i,
// to_i] indicates that the i^th trip has numPassengers_i passengers and the locations to pick them
// up and drop them off are from_i and to_i respectively. The locations are given as the number of
// kilometers due east from the car's initial location.
//
// Return true if it is possible to pick up and drop off all passengers for all the given trips, or
// false otherwise.
//
// Example 1:
//
// Input: trips = [[2,1,5],[3,3,7]], capacity = 4
// Output: false
//
// Example 2:
//
// Input: trips = [[2,1,5],[3,3,7]], capacity = 5
// Output: true
//
// Constraints:
//
// - 1 <= trips.length <= 1000
//
// - trips[i].length == 3
//
// - 1 <= numPassengers_i <= 100
//
// - 0 <= from_i < to_i <= 1000
//
// - 1 <= capacity <= 10^5
//
// Related Topics: Array, Sorting, Heap (Priority Queue), Simulation, Prefix Sum

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
